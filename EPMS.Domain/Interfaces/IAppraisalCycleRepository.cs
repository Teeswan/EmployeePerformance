using EPMS.Infrastructure;

namespace EPMS.Domain.Interfaces;

public interface IAppraisalCycleRepository
{
    Task<IEnumerable<AppraisalCycle>> GetAllAsync();
    Task<AppraisalCycle?> GetByIdAsync(int cycleId);
    Task<AppraisalCycle> CreateAsync(AppraisalCycle entity);
    Task<AppraisalCycle?> UpdateAsync(AppraisalCycle entity);
    Task<bool> DeleteAsync(int cycleId);
}
