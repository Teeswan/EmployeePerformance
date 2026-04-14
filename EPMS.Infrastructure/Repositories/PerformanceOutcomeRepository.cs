using EPMS.Domain.Interfaces;
using EPMS.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EPMS.Infrastructure.Repositories;

public class PerformanceOutcomeRepository : IPerformanceOutcomeRepository
{
    private readonly AppDbContext _context;

    public PerformanceOutcomeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PerformanceOutcome>> GetAllAsync()
    {
        return await _context.PerformanceOutcomes.ToListAsync();
    }

    public async Task<PerformanceOutcome?> GetByIdAsync(int outcomeId)
    {
        return await _context.PerformanceOutcomes.FindAsync(outcomeId);
    }

    public async Task<IEnumerable<PerformanceOutcome>> GetByEmployeeIdAsync(int employeeId)
    {
        return await _context.PerformanceOutcomes
            .Where(po => po.EmployeeId == employeeId)
            .ToListAsync();
    }

    public async Task<IEnumerable<PerformanceOutcome>> GetByCycleIdAsync(int cycleId)
    {
        return await _context.PerformanceOutcomes
            .Where(po => po.CycleId == cycleId)
            .ToListAsync();
    }

    public async Task<PerformanceOutcome> CreateAsync(PerformanceOutcome entity)
    {
        _context.PerformanceOutcomes.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<PerformanceOutcome?> UpdateAsync(PerformanceOutcome entity)
    {
        var existing = await _context.PerformanceOutcomes.FindAsync(entity.OutcomeId);
        if (existing == null) return null;

        _context.Entry(existing).CurrentValues.SetValues(entity);
        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int outcomeId)
    {
        var entity = await _context.PerformanceOutcomes.FindAsync(outcomeId);
        if (entity == null) return false;

        _context.PerformanceOutcomes.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}
