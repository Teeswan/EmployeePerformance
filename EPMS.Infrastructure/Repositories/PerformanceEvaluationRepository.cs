using EPMS.Domain.Interfaces;
using EPMS.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EPMS.Infrastructure.Repositories;

public class PerformanceEvaluationRepository : IPerformanceEvaluationRepository
{
    private readonly AppDbContext _context;

    public PerformanceEvaluationRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PerformanceEvaluation>> GetAllAsync()
    {
        return await _context.PerformanceEvaluations.ToListAsync();
    }

    public async Task<PerformanceEvaluation?> GetByIdAsync(int evalId)
    {
        return await _context.PerformanceEvaluations.FindAsync(evalId);
    }

    public async Task<IEnumerable<PerformanceEvaluation>> GetByEmployeeIdAsync(int employeeId)
    {
        return await _context.PerformanceEvaluations
            .Where(pe => pe.EmployeeId == employeeId)
            .ToListAsync();
    }

    public async Task<IEnumerable<PerformanceEvaluation>> GetByCycleIdAsync(int cycleId)
    {
        return await _context.PerformanceEvaluations
            .Where(pe => pe.CycleId == cycleId)
            .ToListAsync();
    }

    public async Task<PerformanceEvaluation> CreateAsync(PerformanceEvaluation entity)
    {
        _context.PerformanceEvaluations.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<PerformanceEvaluation?> UpdateAsync(PerformanceEvaluation entity)
    {
        var existing = await _context.PerformanceEvaluations.FindAsync(entity.EvalId);
        if (existing == null) return null;

        _context.Entry(existing).CurrentValues.SetValues(entity);
        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int evalId)
    {
        var entity = await _context.PerformanceEvaluations.FindAsync(evalId);
        if (entity == null) return false;

        _context.PerformanceEvaluations.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}
