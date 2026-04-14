using EPMS.Domain.Interfaces;
using EPMS.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EPMS.Infrastructure.Repositories;

public class AppraisalFormRepository : IAppraisalFormRepository
{
    private readonly AppDbContext _context;

    public AppraisalFormRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ApplicationForm>> GetAllAsync()
    {
        return await _context.ApplicationForms.ToListAsync();
    }

    public async Task<ApplicationForm?> GetByIdAsync(int formId)
    {
        return await _context.ApplicationForms.FindAsync(formId);
    }

    public async Task<ApplicationForm> CreateAsync(ApplicationForm entity)
    {
        _context.ApplicationForms.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<ApplicationForm?> UpdateAsync(ApplicationForm entity)
    {
        var existing = await _context.ApplicationForms.FindAsync(entity.FormId);
        if (existing == null) return null;

        _context.Entry(existing).CurrentValues.SetValues(entity);
        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int formId)
    {
        var entity = await _context.ApplicationForms.FindAsync(formId);
        if (entity == null) return false;

        _context.ApplicationForms.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}
