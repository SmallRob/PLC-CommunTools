using Microsoft.EntityFrameworkCore;
using Commun.Data;
using Commun.Data.Entities;
using Commun.Data.Repositories;
using Commun.Server.API.Models;
using Commun.Server.API.Services;

namespace Commun.Server.Tests;

public class DeviceServiceTests : IDisposable
{
    private readonly CommunDbContext _context;
    private readonly DeviceRepository _repository;
    private readonly DeviceService _service;

    public DeviceServiceTests()
    {
        var options = new DbContextOptionsBuilder<CommunDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new CommunDbContext(options);
        _repository = new DeviceRepository(_context);
        _service = new DeviceService(_repository);
    }

    [Fact]
    public async Task CreateDeviceAsync_ShouldReturnCreatedDevice()
    {
        var dto = new CreateDeviceDto
        {
            Name = "Test Device",
            Protocol = "Modbus",
            Address = "192.168.1.100",
            Port = 502
        };

        var result = await _service.CreateDeviceAsync(dto);

        Assert.NotNull(result.Id);
        Assert.Equal("Test Device", result.Name);
        Assert.Equal("Modbus", result.Protocol);
        Assert.Equal("192.168.1.100", result.Address);
        Assert.Equal(502, result.Port);
    }

    [Fact]
    public async Task CreateDeviceAsync_WithConfig_ShouldSerializeConfig()
    {
        var dto = new CreateDeviceDto
        {
            Name = "Config Device",
            Protocol = "Modbus",
            Address = "192.168.1.100",
            Port = 502,
            Config = new Dictionary<string, object> { { "unitId", 1 } }
        };

        var result = await _service.CreateDeviceAsync(dto);

        Assert.NotNull(result.Config);
        Assert.True(result.Config.ContainsKey("unitId"));
    }

    [Fact]
    public async Task GetAllDevicesAsync_ShouldReturnAllDevices()
    {
        await _service.CreateDeviceAsync(new CreateDeviceDto { Name = "Device 1", Protocol = "Modbus", Address = "192.168.1.1", Port = 502 });
        await _service.CreateDeviceAsync(new CreateDeviceDto { Name = "Device 2", Protocol = "MQTT", Address = "192.168.1.2", Port = 1883 });

        var devices = await _service.GetAllDevicesAsync();

        Assert.Equal(2, devices.Count());
    }

    [Fact]
    public async Task GetDeviceAsync_ExistingDevice_ShouldReturnDevice()
    {
        var created = await _service.CreateDeviceAsync(new CreateDeviceDto { Name = "Test", Protocol = "Modbus", Address = "10.0.0.1", Port = 502 });

        var result = await _service.GetDeviceAsync(created.Id!);

        Assert.NotNull(result);
        Assert.Equal("Test", result.Name);
    }

    [Fact]
    public async Task GetDeviceAsync_NonExistingDevice_ShouldReturnNull()
    {
        var result = await _service.GetDeviceAsync("non-existing-id");

        Assert.Null(result);
    }

    [Fact]
    public async Task UpdateDeviceAsync_ExistingDevice_ShouldUpdate()
    {
        var created = await _service.CreateDeviceAsync(new CreateDeviceDto { Name = "Old Name", Protocol = "Modbus", Address = "10.0.0.1", Port = 502 });

        var updateDto = new DeviceDto
        {
            Name = "New Name",
            Protocol = "Modbus",
            Address = "10.0.0.2",
            Port = 503
        };

        await _service.UpdateDeviceAsync(created.Id!, updateDto);

        var updated = await _service.GetDeviceAsync(created.Id!);
        Assert.Equal("New Name", updated!.Name);
        Assert.Equal("10.0.0.2", updated.Address);
        Assert.Equal(503, updated.Port);
    }

    [Fact]
    public async Task UpdateDeviceAsync_NonExistingDevice_ShouldThrow()
    {
        var dto = new DeviceDto { Name = "Test", Protocol = "Modbus", Address = "10.0.0.1", Port = 502 };

        await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.UpdateDeviceAsync("non-existing", dto));
    }

    [Fact]
    public async Task DeleteDeviceAsync_ShouldRemoveDevice()
    {
        var created = await _service.CreateDeviceAsync(new CreateDeviceDto { Name = "To Delete", Protocol = "Modbus", Address = "10.0.0.1", Port = 502 });

        await _service.DeleteDeviceAsync(created.Id!);

        var result = await _service.GetDeviceAsync(created.Id!);
        Assert.Null(result);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
