using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EPMS.Domain.Entities;
using EPMS.Domain.Interfaces;
using EPMS.Infrastructure.Contexts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using static EPMS.Infrastructure.StoredProcedures;

namespace EPMS.Infrastructure.Repositories;

public class AppraisalResponseRepository : BaseRepository<AppraisalResponse, long>, IAppraisalResponseRepository
{
    private readonly ISqlRepository<AppraisalResponse, long> _sqlRepository;

    public AppraisalResponseRepository(AppDbContext context, ISqlRepository<AppraisalResponse, long> sqlRepository) : base(context)
    {
        _sqlRepository = sqlRepository;
    }

    public override async Task<IEnumerable<AppraisalResponse>> GetAllAsync()
    {
        return await _sqlRepository.FromSqlAsync(AppraisalResponses_GetAll);
    }

    public override async Task<AppraisalResponse?> GetByIdAsync(long responseId)
    {
        return await _sqlRepository.FromSqlFirstOrDefaultAsync(AppraisalResponses_GetById, new SqlParameter("@ResponseID", responseId));
    }

    public override async Task<AppraisalResponse> CreateAsync(AppraisalResponse entity)
    {
        var parameters = new object[]
        {
            new SqlParameter("@EvalID", (object?)entity.EvalId ?? DBNull.Value),
            new SqlParameter("@QuestionID", (object?)entity.QuestionId ?? DBNull.Value),
            new SqlParameter("@RespondentID", (object?)entity.RespondentId ?? DBNull.Value),
            new SqlParameter("@AnswerText", (object?)entity.AnswerText ?? DBNull.Value),
            new SqlParameter("@RatingValue", (object?)entity.RatingValue ?? DBNull.Value),
            new SqlParameter("@IsAnonymous", (object?)entity.IsAnonymous ?? DBNull.Value)
        };

        var result = await _sqlRepository.FromSqlFirstOrDefaultAsync(AppraisalResponses_Create, parameters);
        return result ?? throw new InvalidOperationException("Failed to create appraisal response.");
    }

    public override async Task<AppraisalResponse?> UpdateAsync(AppraisalResponse entity)
    {
        var parameters = new object[]
        {
            new SqlParameter("@ResponseID", entity.ResponseId),
            new SqlParameter("@EvalID", (object?)entity.EvalId ?? DBNull.Value),
            new SqlParameter("@QuestionID", (object?)entity.QuestionId ?? DBNull.Value),
            new SqlParameter("@RespondentID", (object?)entity.RespondentId ?? DBNull.Value),
            new SqlParameter("@AnswerText", (object?)entity.AnswerText ?? DBNull.Value),
            new SqlParameter("@RatingValue", (object?)entity.RatingValue ?? DBNull.Value),
            new SqlParameter("@IsAnonymous", (object?)entity.IsAnonymous ?? DBNull.Value)
        };

        return await _sqlRepository.FromSqlFirstOrDefaultAsync(AppraisalResponses_Update, parameters);
    }

    public override async Task<bool> DeleteAsync(long responseId)
    {
        var rowsAffected = await _sqlRepository.ExecuteSqlAsync(AppraisalResponses_Delete, new SqlParameter("@ResponseID", responseId));
        return rowsAffected > 0;
    }

    public async Task<IEnumerable<AppraisalResponse>> GetByEvalIdAsync(int evalId)
    {
        return await _sqlRepository.FromSqlAsync(AppraisalResponses_GetByEvalId, new SqlParameter("@EvalID", evalId));
    }
}
