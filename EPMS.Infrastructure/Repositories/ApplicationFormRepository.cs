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

public class ApplicationFormRepository : BaseRepository<ApplicationForm, int>, IAppraisalFormRepository
{
    private readonly ISqlRepository<ApplicationForm, int> _sqlRepository;

    public ApplicationFormRepository(AppDbContext context, ISqlRepository<ApplicationForm, int> sqlRepository) : base(context)
    {
        _sqlRepository = sqlRepository;
    }

    public override async Task<IEnumerable<ApplicationForm>> GetAllAsync()
    {
        return await _sqlRepository.FromSqlAsync(ApplicationForms_GetAll);
    }

    public override async Task<ApplicationForm?> GetByIdAsync(int formId)
    {
        return await _sqlRepository.FromSqlFirstOrDefaultAsync(ApplicationForms_GetById, new SqlParameter("@FormID", formId));
    }

    public override async Task<ApplicationForm> CreateAsync(ApplicationForm entity)
    {
        var parameters = new object[]
        {
            new SqlParameter("@FormName", (object?)entity.FormName ?? DBNull.Value),
            new SqlParameter("@IsActive", (object?)entity.IsActive ?? DBNull.Value)
        };

        var result = await _sqlRepository.FromSqlFirstOrDefaultAsync(ApplicationForms_Create, parameters);
        return result ?? throw new InvalidOperationException("Failed to create appraisal form.");
    }

    public override async Task<ApplicationForm?> UpdateAsync(ApplicationForm entity)
    {
        var parameters = new object[]
        {
            new SqlParameter("@FormID", entity.FormId),
            new SqlParameter("@FormName", (object?)entity.FormName ?? DBNull.Value),
            new SqlParameter("@IsActive", (object?)entity.IsActive ?? DBNull.Value)
        };

        return await _sqlRepository.FromSqlFirstOrDefaultAsync(ApplicationForms_Update, parameters);
    }

    public override async Task<bool> DeleteAsync(int formId)
    {
        var rowsAffected = await _sqlRepository.ExecuteSqlAsync(ApplicationForms_Delete, new SqlParameter("@FormID", formId));
        return rowsAffected > 0;
    }
}
