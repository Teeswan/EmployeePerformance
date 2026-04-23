using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EPMS.Domain.Entities;
using EPMS.Domain.Interfaces;
using EPMS.Infrastructure.Contexts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using static EPMS.Infrastructure.StoredProcedures;

namespace EPMS.Infrastructure.Repositories;

public class AppraisalQuestionRepository : BaseRepository<AppraisalQuestion, int>, IAppraisalQuestionRepository
{
    private readonly ISqlRepository<AppraisalQuestion, int> _sqlRepository;

    public AppraisalQuestionRepository(AppDbContext context, ISqlRepository<AppraisalQuestion, int> sqlRepository) : base(context)
    {
        _sqlRepository = sqlRepository;
    }

    public override async Task<IEnumerable<AppraisalQuestion>> GetAllAsync()
    {
        return await _sqlRepository.FromSqlAsync(AppraisalQuestions_GetAll);
    }

    public override async Task<AppraisalQuestion?> GetByIdAsync(int questionId)
    {
        return await _sqlRepository.FromSqlFirstOrDefaultAsync(AppraisalQuestions_GetById, new SqlParameter("@QuestionID", questionId));
    }

    public override async Task<AppraisalQuestion> CreateAsync(AppraisalQuestion entity)
    {
        var parameters = new object[]
        {
            new SqlParameter("@QuestionText", entity.QuestionText),
            new SqlParameter("@Category", (object?)entity.Category ?? DBNull.Value),
            new SqlParameter("@IsRequired", (object?)entity.IsRequired ?? DBNull.Value)
        };

        var result = await _sqlRepository.FromSqlFirstOrDefaultAsync(AppraisalQuestions_Create, parameters);
        return result ?? throw new InvalidOperationException("Failed to create appraisal question.");
    }

    public override async Task<AppraisalQuestion?> UpdateAsync(AppraisalQuestion entity)
    {
        var parameters = new object[]
        {
            new SqlParameter("@QuestionID", entity.QuestionId),
            new SqlParameter("@QuestionText", entity.QuestionText),
            new SqlParameter("@Category", (object?)entity.Category ?? DBNull.Value),
            new SqlParameter("@IsRequired", (object?)entity.IsRequired ?? DBNull.Value)
        };

        return await _sqlRepository.FromSqlFirstOrDefaultAsync(AppraisalQuestions_Update, parameters);
    }

    public override async Task<bool> DeleteAsync(int questionId)
    {
        var rowsAffected = await _sqlRepository.ExecuteSqlAsync(AppraisalQuestions_Delete, new SqlParameter("@QuestionID", questionId));
        return rowsAffected > 0;
    }
}
