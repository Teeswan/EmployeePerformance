using EPMS.Infrastructure;

namespace EPMS.Domain.Interfaces;

public interface IAppraisalResponseRepository
{
    Task<IEnumerable<AppraisalResponse>> GetAllAsync();
    Task<AppraisalResponse?> GetByIdAsync(long responseId);
    Task<IEnumerable<AppraisalResponse>> GetByEvalIdAsync(int evalId);
    Task<AppraisalResponse> CreateAsync(AppraisalResponse entity);
    Task<AppraisalResponse?> UpdateAsync(AppraisalResponse entity);
    Task<bool> DeleteAsync(long responseId);
}
