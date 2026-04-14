using EPMS.Domain.Interfaces;
using EPMS.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EPMS.Infrastructure.Repositories;

public class AppraisalResponseRepository : IAppraisalResponseRepository
{
    private readonly AppDbContext _context;

    public AppraisalResponseRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AppraisalResponse>> GetAllAsync()
    {
        return await _context.AppraisalResponses.ToListAsync();
    }

    public async Task<AppraisalResponse?> GetByIdAsync(long responseId)
    {
        return await _context.AppraisalResponses.FindAsync(responseId);
    }

    public async Task<IEnumerable<AppraisalResponse>> GetByEvalIdAsync(int evalId)
    {
        return await _context.AppraisalResponses
            .Where(r => r.EvalId == evalId)
            .ToListAsync();
    }

    public async Task<AppraisalResponse> CreateAsync(AppraisalResponse entity)
    {
        _context.AppraisalResponses.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<AppraisalResponse?> UpdateAsync(AppraisalResponse entity)
    {
        var existing = await _context.AppraisalResponses.FindAsync(entity.ResponseId);
        if (existing == null) return null;

        _context.Entry(existing).CurrentValues.SetValues(entity);
        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(long responseId)
    {
        var entity = await _context.AppraisalResponses.FindAsync(responseId);
        if (entity == null) return false;

        _context.AppraisalResponses.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}
