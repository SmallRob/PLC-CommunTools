using System.Text.Json;
using Commun.Data.Entities;
using Commun.Data.Repositories;
using Commun.Server.API.Models;

namespace Commun.Server.API.Services;

public class DeviceService
{
    private readonly DeviceRepository _repository;

    public DeviceService(DeviceRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<DeviceDto>> GetAllDevicesAsync()
    {
        var devices = await _repository.GetAllAsync();
        return devices.Select(MapToDto);
    }

    public async Task<DeviceDto?> GetDeviceAsync(string id)
    {
        var device = await _repository.GetByIdAsync(id);
        return device != null ? MapToDto(device) : null;
    }

    public async Task<DeviceDto> CreateDeviceAsync(CreateDeviceDto dto)
    {
        var device = new Device
        {
            Name = dto.Name,
            Protocol = dto.Protocol,
            Address = dto.Address,
            Port = dto.Port,
            Config = dto.Config != null ? JsonSerializer.Serialize(dto.Config) : null
        };

        await _repository.AddAsync(device);
        return MapToDto(device);
    }

    public async Task UpdateDeviceAsync(string id, DeviceDto dto)
    {
        var device = await _repository.GetByIdAsync(id);
        if (device == null)
        {
            throw new KeyNotFoundException($"Device {id} not found");
        }

        device.Name = dto.Name;
        device.Protocol = dto.Protocol;
        device.Address = dto.Address;
        device.Port = dto.Port;

        await _repository.UpdateAsync(device);
    }

    public async Task DeleteDeviceAsync(string id)
    {
        await _repository.DeleteAsync(id);
    }

    private static DeviceDto MapToDto(Device device)
    {
        return new DeviceDto
        {
            Id = device.Id,
            Name = device.Name,
            Protocol = device.Protocol,
            Address = device.Address,
            Port = device.Port,
            Config = device.Config != null
                ? JsonSerializer.Deserialize<Dictionary<string, object>>(device.Config)
                : null
        };
    }
}
