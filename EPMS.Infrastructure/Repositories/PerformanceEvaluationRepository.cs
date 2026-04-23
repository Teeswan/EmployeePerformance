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

public class PerformanceEvaluationRepository : BaseRepository<PerformanceEvaluation, int>, IPerformanceEvaluationRepository
{
    private readonly ISqlRepository<PerformanceEvaluation, int> _sqlRepository;

    public PerformanceEvaluationRepository(AppDbContext context, ISqlRepository<PerformanceEvaluation, int> sqlRepository) : base(context)
    {
        _sqlRepository = sqlRepository;
    }

    public override async Task<IEnumerable<PerformanceEvaluation>> GetAllAsync()
    {
        return await _sqlRepository.FromSqlAsync(PerformanceEvaluations_GetAll);
    }

    public override async Task<PerformanceEvaluation?> GetByIdAsync(int evalId)
    {
        return await _sqlRepository.FromSqlFirstOrDefaultAsync(PerformanceEvaluations_GetById, new SqlParameter("@EvalID", evalId));
    }

    public override async Task<PerformanceEvaluation> CreateAsync(PerformanceEvaluation entity)
    {
        var parameters = new object[]
        {
            new SqlParameter("@EmployeeID", (object?)entity.EmployeeId ?? DBNull.Value),
            new SqlParameter("@CycleID", (object?)entity.CycleId ?? DBNull.Value),
            new SqlParameter("@SelfRating", (object?)entity.SelfRating ?? DBNull.Value),
            new SqlParameter("@ManagerRating", (object?)entity.ManagerRating ?? DBNull.Value),
            new SqlParameter("@SelfComments", (object?)entity.SelfComments ?? DBNull.Value),
            new SqlParameter("@ManagerComments", (object?)entity.ManagerComments ?? DBNull.Value),
            new SqlParameter("@FinalRatingScore", (object?)entity.FinalRatingScore ?? DBNull.Value),
            new SqlParameter("@IsFinalized", (object?)entity.IsFinalized ?? DBNull.Value),
            new SqlParameter("@FinalizedAt", (object?)entity.FinalizedAt ?? DBNull.Value)
        };

        var result = await _sqlRepository.FromSqlFirstOrDefaultAsync(PerformanceEvaluations_Create, parameters);
        return result ?? throw new InvalidOperationException("Failed to create performance evaluation.");
    }

    public override async Task<PerformanceEvaluation?> UpdateAsync(PerformanceEvaluation entity)
    {
        var parameters = new object[]
        {
            new SqlParameter("@EvalID", entity.EvalId),
            new SqlParameter("@EmployeeID", (object?)entity.EmployeeId ?? DBNull.Value),
            new SqlParameter("@CycleID", (object?)entity.CycleId ?? DBNull.Value),
            new SqlParameter("@SelfRating", (object?)entity.SelfRating ?? DBNull.Value),
            new SqlParameter("@ManagerRating", (object?)entity.ManagerRating ?? DBNull.Value),
            new SqlParameter("@SelfComments", (object?)entity.SelfComments ?? DBNull.Value),
            new SqlParameter("@ManagerComments", (object?)entity.ManagerComments ?? DBNull.Value),
            new SqlParameter("@FinalRatingScore", (object?)entity.FinalRatingScore ?? DBNull.Value),
            new SqlParameter("@IsFinalized", (object?)entity.IsFinalized ?? DBNull.Value),
            new SqlParameter("@FinalizedAt", (object?)entity.FinalizedAt ?? DBNull.Value)
        };

        return await _sqlRepository.FromSqlFirstOrDefaultAsync(PerformanceEvaluations_Update, parameters);
    }

    public override async Task<bool> DeleteAsync(int evalId)
    {
        var rowsAffected = await _sqlRepository.ExecuteSqlAsync(PerformanceEvaluations_Delete, new SqlParameter("@EvalID", evalId));
        return rowsAffected > 0;
    }

    public async Task<IEnumerable<PerformanceEvaluation>> GetByEmployeeIdAsync(int employeeId)
    {
        return await _sqlRepository.FromSqlAsync(PerformanceEvaluations_GetByEmployeeId, new SqlParameter("@EmployeeID", employeeId));
    }

    public async Task<IEnumerable<PerformanceEvaluation>> GetByCycleIdAsync(int cycleId)
    {
        return await _sqlRepository.FromSqlAsync(PerformanceEvaluations_GetByCycleId, new SqlParameter("@CycleID", cycleId));
    }
}
