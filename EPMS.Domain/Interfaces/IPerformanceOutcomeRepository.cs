using EPMS.Infrastructure;

namespace EPMS.Domain.Interfaces;

public interface IPerformanceOutcomeRepository
{
    Task<IEnumerable<PerformanceOutcome>> GetAllAsync();
    Task<PerformanceOutcome?> GetByIdAsync(int outcomeId);
    Task<IEnumerable<PerformanceOutcome>> GetByEmployeeIdAsync(int employeeId);
    Task<IEnumerable<PerformanceOutcome>> GetByCycleIdAsync(int cycleId);
    Task<PerformanceOutcome> CreateAsync(PerformanceOutcome entity);
    Task<PerformanceOutcome?> UpdateAsync(PerformanceOutcome entity);
    Task<bool> DeleteAsync(int outcomeId);
}
