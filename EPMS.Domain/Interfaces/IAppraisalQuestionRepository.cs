using EPMS.Infrastructure;

namespace EPMS.Domain.Interfaces;

public interface IAppraisalQuestionRepository
{
    Task<IEnumerable<AppraisalQuestion>> GetAllAsync();
    Task<AppraisalQuestion?> GetByIdAsync(int questionId);
    Task<AppraisalQuestion> CreateAsync(AppraisalQuestion entity);
    Task<AppraisalQuestion?> UpdateAsync(AppraisalQuestion entity);
    Task<bool> DeleteAsync(int questionId);
}
