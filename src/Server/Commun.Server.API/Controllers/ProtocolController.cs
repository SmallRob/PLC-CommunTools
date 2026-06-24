using Microsoft.AspNetCore.Mvc;
using Commun.Protocols;
using Commun.Server.API.Models;
using Commun.Server.API.Services;

namespace Commun.Server.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProtocolController : ControllerBase
{
    private readonly ProtocolService _protocolService;

    public ProtocolController(ProtocolService protocolService)
    {
        _protocolService = protocolService;
    }

    [HttpGet("protocols")]
    public ActionResult<IEnumerable<ProtocolMetadata>> GetAvailableProtocols()
    {
        var protocols = _protocolService.GetAvailableProtocols();
        return Ok(protocols);
    }

    [HttpPost("{deviceId}/connect")]
    public async Task<IActionResult> Connect(string deviceId, [FromBody] ConnectRequest request)
    {
        var config = new ProtocolConfig
        {
            ProtocolName = request.Protocol,
            Address = request.Address,
            Port = request.Port
        };

        var result = await _protocolService.ConnectAsync(deviceId, request.Protocol, config);

        if (result)
        {
            return Ok(new { Message = "Connected successfully" });
        }

        return BadRequest(new { Message = "Connection failed" });
    }

    [HttpPost("{deviceId}/disconnect")]
    public async Task<IActionResult> Disconnect(string deviceId)
    {
        await _protocolService.DisconnectAsync(deviceId);
        return Ok(new { Message = "Disconnected successfully" });
    }

    [HttpPost("{deviceId}/send")]
    public async Task<ActionResult<ProtocolResponse>> Send(string deviceId, ProtocolRequestDto dto)
    {
        var request = new ProtocolRequest
        {
            CommandType = dto.CommandType,
            Register = dto.Register,
            Length = dto.Length,
            Data = dto.Data,
            Parameters = dto.Parameters ?? new Dictionary<string, object>()
        };

        var response = await _protocolService.SendAsync(deviceId, request);
        return Ok(response);
    }

    [HttpGet("{deviceId}/status")]
    public ActionResult<bool> GetStatus(string deviceId)
    {
        var isConnected = _protocolService.IsConnected(deviceId);
        return Ok(new { Connected = isConnected });
    }
}
