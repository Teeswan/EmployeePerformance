using EPMS.Domain.Interfaces;
using EPMS.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EPMS.Infrastructure.Repositories;

public class FormQuestionRepository : IFormQuestionRepository
{
    private readonly AppDbContext _context;

    public FormQuestionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<FormQuestion>> GetAllAsync()
    {
        return await _context.FormQuestions.ToListAsync();
    }

    public async Task<IEnumerable<FormQuestion>> GetByFormIdAsync(int formId)
    {
        return await _context.FormQuestions
            .Where(fq => fq.FormId == formId)
            .OrderBy(fq => fq.SortOrder)
            .ToListAsync();
    }

    public async Task<FormQuestion?> GetByFormAndQuestionIdAsync(int formId, int questionId)
    {
        return await _context.FormQuestions
            .FirstOrDefaultAsync(fq => fq.FormId == formId && fq.QuestionId == questionId);
    }

    public async Task<FormQuestion> CreateAsync(FormQuestion entity)
    {
        _context.FormQuestions.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<FormQuestion?> UpdateAsync(FormQuestion entity)
    {
        var existing = await _context.FormQuestions
            .FirstOrDefaultAsync(fq => fq.FormId == entity.FormId && fq.QuestionId == entity.QuestionId);
        if (existing == null) return null;

        _context.Entry(existing).CurrentValues.SetValues(entity);
        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int formId, int questionId)
    {
        var entity = await _context.FormQuestions
            .FirstOrDefaultAsync(fq => fq.FormId == formId && fq.QuestionId == questionId);
        if (entity == null) return false;

        _context.FormQuestions.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}
