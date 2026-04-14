using EPMS.Domain.Interfaces;
using EPMS.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EPMS.Infrastructure.Repositories;

public class AppraisalQuestionRepository : IAppraisalQuestionRepository
{
    private readonly AppDbContext _context;

    public AppraisalQuestionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AppraisalQuestion>> GetAllAsync()
    {
        return await _context.AppraisalQuestions.ToListAsync();
    }

    public async Task<AppraisalQuestion?> GetByIdAsync(int questionId)
    {
        return await _context.AppraisalQuestions.FindAsync(questionId);
    }

    public async Task<AppraisalQuestion> CreateAsync(AppraisalQuestion entity)
    {
        _context.AppraisalQuestions.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<AppraisalQuestion?> UpdateAsync(AppraisalQuestion entity)
    {
        var existing = await _context.AppraisalQuestions.FindAsync(entity.QuestionId);
        if (existing == null) return null;

        _context.Entry(existing).CurrentValues.SetValues(entity);
        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int questionId)
    {
        var entity = await _context.AppraisalQuestions.FindAsync(questionId);
        if (entity == null) return false;

        _context.AppraisalQuestions.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}
