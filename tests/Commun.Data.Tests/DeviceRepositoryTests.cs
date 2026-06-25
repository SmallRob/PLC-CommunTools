using Microsoft.EntityFrameworkCore;
using Commun.Data;
using Commun.Data.Entities;
using Commun.Data.Repositories;
using Xunit;

namespace Commun.Data.Tests;

public class DeviceRepositoryTests : IDisposable
{
    private readonly CommunDbContext _context;
    private readonly DeviceRepository _repository;

    public DeviceRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<CommunDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new CommunDbContext(options);
        _repository = new DeviceRepository(_context);
    }

    [Fact]
    public async Task AddAsync_ShouldAddDevice()
    {
        var device = new Device
        {
            Name = "Test Device",
            Protocol = "Modbus",
            Address = "192.168.1.100",
            Port = 502
        };

        await _repository.AddAsync(device);

        var result = await _repository.GetByIdAsync(device.Id);
        Assert.NotNull(result);
        Assert.Equal("Test Device", result.Name);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllDevices()
    {
        await _repository.AddAsync(new Device { Name = "Device 1", Protocol = "Modbus", Address = "192.168.1.1", Port = 502 });
        await _repository.AddAsync(new Device { Name = "Device 2", Protocol = "MQTT", Address = "192.168.1.2", Port = 1883 });

        var devices = await _repository.GetAllAsync();

        Assert.Equal(2, devices.Count());
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateDevice()
    {
        var device = new Device
        {
            Name = "Original Name",
            Protocol = "Modbus",
            Address = "192.168.1.100",
            Port = 502
        };
        await _repository.AddAsync(device);

        device.Name = "Updated Name";
        await _repository.UpdateAsync(device);

        var result = await _repository.GetByIdAsync(device.Id);
        Assert.NotNull(result);
        Assert.Equal("Updated Name", result.Name);
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveDevice()
    {
        var device = new Device
        {
            Name = "To Delete",
            Protocol = "Modbus",
            Address = "192.168.1.100",
            Port = 502
        };
        await _repository.AddAsync(device);

        await _repository.DeleteAsync(device.Id);

        var result = await _repository.GetByIdAsync(device.Id);
        Assert.Null(result);
    }

    [Fact]
    public async Task ExistsAsync_ShouldReturnTrueForExistingDevice()
    {
        var device = new Device
        {
            Name = "Existing Device",
            Protocol = "Modbus",
            Address = "192.168.1.100",
            Port = 502
        };
        await _repository.AddAsync(device);

        var exists = await _repository.ExistsAsync(device.Id);

        Assert.True(exists);
    }

    [Fact]
    public async Task ExistsAsync_ShouldReturnFalseForNonExistingDevice()
    {
        var exists = await _repository.ExistsAsync("non-existing-id");

        Assert.False(exists);
    }

    [Fact]
    public async Task SearchAsync_ShouldFilterByName()
    {
        await _repository.AddAsync(new Device { Name = "Modbus Gateway", Protocol = "Modbus", Address = "192.168.1.1", Port = 502 });
        await _repository.AddAsync(new Device { Name = "MQTT Broker", Protocol = "MQTT", Address = "192.168.1.2", Port = 1883 });

        var results = await _repository.SearchAsync("Modbus", null);

        Assert.Single(results);
        Assert.Equal("Modbus Gateway", results.First().Name);
    }

    [Fact]
    public async Task SearchAsync_ShouldFilterByProtocol()
    {
        await _repository.AddAsync(new Device { Name = "Device 1", Protocol = "Modbus", Address = "192.168.1.1", Port = 502 });
        await _repository.AddAsync(new Device { Name = "Device 2", Protocol = "MQTT", Address = "192.168.1.2", Port = 1883 });
        await _repository.AddAsync(new Device { Name = "Device 3", Protocol = "Modbus", Address = "192.168.1.3", Port = 502 });

        var results = await _repository.SearchAsync(null, "Modbus");

        Assert.Equal(2, results.Count());
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
