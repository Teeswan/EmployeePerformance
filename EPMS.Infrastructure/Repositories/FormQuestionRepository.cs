using System;
using EPMS.Domain.Entities;
using EPMS.Domain.Interfaces;
using EPMS.Infrastructure.Contexts;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static EPMS.Infrastructure.StoredProcedures;

namespace EPMS.Infrastructure.Repositories;

public class FormQuestionRepository : IFormQuestionRepository
{
    private readonly AppDbContext _context;
    private readonly ISqlRepository<FormQuestion, object[]> _sqlRepository;

    public FormQuestionRepository(AppDbContext context, ISqlRepository<FormQuestion, object[]> sqlRepository)
    {
        _context = context;
        _sqlRepository = sqlRepository;
    }

    public async Task<IEnumerable<FormQuestion>> GetAllAsync()
    {
        return await _sqlRepository.FromSqlAsync(FormQuestions_GetAll);
    }

    public async Task<IEnumerable<FormQuestion>> GetByFormIdAsync(int formId)
    {
        return await _sqlRepository.FromSqlAsync(FormQuestions_GetByFormId, new SqlParameter("@FormID", formId));
    }

    public async Task<FormQuestion?> GetByFormAndQuestionIdAsync(int formId, int questionId)
    {
        var parameters = new object[]
        {
            new SqlParameter("@FormID", formId),
            new SqlParameter("@QuestionID", questionId)
        };
        return await _sqlRepository.FromSqlFirstOrDefaultAsync(FormQuestions_GetByFormAndQuestionId, parameters);
    }

    public async Task<FormQuestion> CreateAsync(FormQuestion entity)
    {
        var parameters = new object[]
        {
            new SqlParameter("@FormID", (object?)entity.FormId ?? DBNull.Value),
            new SqlParameter("@QuestionID", (object?)entity.QuestionId ?? DBNull.Value),
            new SqlParameter("@SortOrder", (object?)entity.SortOrder ?? DBNull.Value)
        };

        var result = await _sqlRepository.FromSqlFirstOrDefaultAsync(FormQuestions_Create, parameters);
        return result ?? throw new InvalidOperationException("Failed to create form question.");
    }

    public async Task<FormQuestion?> UpdateAsync(FormQuestion entity)
    {
        var parameters = new object[]
        {
            new SqlParameter("@FormID", (object?)entity.FormId ?? DBNull.Value),
            new SqlParameter("@QuestionID", (object?)entity.QuestionId ?? DBNull.Value),
            new SqlParameter("@SortOrder", (object?)entity.SortOrder ?? DBNull.Value)
        };

        return await _sqlRepository.FromSqlFirstOrDefaultAsync(FormQuestions_Update, parameters);
    }

    public async Task<bool> DeleteAsync(int formId, int questionId)
    {
        var parameters = new object[]
        {
            new SqlParameter("@FormID", formId),
            new SqlParameter("@QuestionID", questionId)
        };
        var rowsAffected = await _sqlRepository.ExecuteSqlAsync(FormQuestions_Delete, parameters);
        return rowsAffected > 0;
    }
}
