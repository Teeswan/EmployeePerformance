using EPMS.Domain.Interfaces;
using EPMS.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EPMS.Infrastructure.Repositories;

public class AppraisalCycleRepository : IAppraisalCycleRepository
{
    private readonly AppDbContext _context;

    public AppraisalCycleRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AppraisalCycle>> GetAllAsync()
    {
        return await _context.AppraisalCycles.ToListAsync();
    }

    public async Task<AppraisalCycle?> GetByIdAsync(int cycleId)
    {
        return await _context.AppraisalCycles.FindAsync(cycleId);
    }

    public async Task<AppraisalCycle> CreateAsync(AppraisalCycle entity)
    {
        _context.AppraisalCycles.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<AppraisalCycle?> UpdateAsync(AppraisalCycle entity)
    {
        var existing = await _context.AppraisalCycles.FindAsync(entity.CycleId);
        if (existing == null) return null;

        _context.Entry(existing).CurrentValues.SetValues(entity);
        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int cycleId)
    {
        var entity = await _context.AppraisalCycles.FindAsync(cycleId);
        if (entity == null) return false;

        _context.AppraisalCycles.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}
