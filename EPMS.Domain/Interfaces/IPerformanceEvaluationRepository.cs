using EPMS.Infrastructure;

namespace EPMS.Domain.Interfaces;

public interface IPerformanceEvaluationRepository
{
    Task<IEnumerable<PerformanceEvaluation>> GetAllAsync();
    Task<PerformanceEvaluation?> GetByIdAsync(int evalId);
    Task<IEnumerable<PerformanceEvaluation>> GetByEmployeeIdAsync(int employeeId);
    Task<IEnumerable<PerformanceEvaluation>> GetByCycleIdAsync(int cycleId);
    Task<PerformanceEvaluation> CreateAsync(PerformanceEvaluation entity);
    Task<PerformanceEvaluation?> UpdateAsync(PerformanceEvaluation entity);
    Task<bool> DeleteAsync(int evalId);
}
