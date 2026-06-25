using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using Commun.Server.API.Models;
using Commun.Protocols;
using Xunit;

namespace Commun.Integration.Tests;

public class EndToEndTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;

    public EndToEndTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetDevices_ReturnsSuccessResponse()
    {
        var response = await _client.GetAsync("/api/devices");
        response.EnsureSuccessStatusCode();

        var devices = await response.Content.ReadFromJsonAsync<IEnumerable<DeviceDto>>();
        Assert.NotNull(devices);
    }

    [Fact]
    public async Task CreateDevice_ReturnsCreatedDevice()
    {
        var dto = new CreateDeviceDto
        {
            Name = "Integration Test Device",
            Protocol = "Modbus",
            Address = "192.168.1.100",
            Port = 502
        };

        var response = await _client.PostAsJsonAsync("/api/devices", dto);
        response.EnsureSuccessStatusCode();

        var device = await response.Content.ReadFromJsonAsync<DeviceDto>();
        Assert.NotNull(device);
        Assert.Equal("Integration Test Device", device.Name);
    }

    [Fact]
    public async Task GetAvailableProtocols_ReturnsProtocols()
    {
        var response = await _client.GetAsync("/api/protocol/protocols");
        response.EnsureSuccessStatusCode();

        var protocols = await response.Content.ReadFromJsonAsync<IEnumerable<ProtocolMetadata>>();
        Assert.NotNull(protocols);
    }
}
