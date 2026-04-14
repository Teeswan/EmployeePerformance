using EPMS.Infrastructure;

namespace EPMS.Domain.Interfaces;

public interface IAppraisalFormRepository
{
    Task<IEnumerable<ApplicationForm>> GetAllAsync();
    Task<ApplicationForm?> GetByIdAsync(int formId);
    Task<ApplicationForm> CreateAsync(ApplicationForm entity);
    Task<ApplicationForm?> UpdateAsync(ApplicationForm entity);
    Task<bool> DeleteAsync(int formId);
}
