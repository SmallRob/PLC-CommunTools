using Microsoft.EntityFrameworkCore;
using Commun.Data.Entities;

namespace Commun.Data.Repositories;

public class DeviceRepository : IRepository<Device>
{
    private readonly CommunDbContext _context;

    public DeviceRepository(CommunDbContext context)
    {
        _context = context;
    }

    public async Task<Device?> GetByIdAsync(string id)
    {
        return await _context.Devices
            .Include(d => d.ConnectionHistory)
            .Include(d => d.DataRecords)
            .FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task<IEnumerable<Device>> GetAllAsync()
    {
        return await _context.Devices.ToListAsync();
    }

    public async Task AddAsync(Device entity)
    {
        await _context.Devices.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Device entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        _context.Devices.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(string id)
    {
        var device = await _context.Devices.FindAsync(id);
        if (device != null)
        {
            _context.Devices.Remove(device);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(string id)
    {
        return await _context.Devices.AnyAsync(d => d.Id == id);
    }

    public async Task<IEnumerable<Device>> SearchAsync(string? name, string? protocol)
    {
        var query = _context.Devices.AsQueryable();

        if (!string.IsNullOrEmpty(name))
        {
            query = query.Where(d => d.Name.Contains(name));
        }

        if (!string.IsNullOrEmpty(protocol))
        {
            query = query.Where(d => d.Protocol == protocol);
        }

        return await query.ToListAsync();
    }
}
