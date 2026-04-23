using System.Collections.Generic;
using System.Threading.Tasks;
using EPMS.Domain.Entities;
using EPMS.Domain.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace EPMS.Infrastructure.Repositories;

public class CachedPerformanceEvaluationRepository : CachedBaseRepository<PerformanceEvaluation, int>, IPerformanceEvaluationRepository
{
    private readonly IPerformanceEvaluationRepository _innerRepository;

    public CachedPerformanceEvaluationRepository(IPerformanceEvaluationRepository innerRepository, IMemoryCache cache, TimeSpan cacheDuration) 
        : base(innerRepository, cache, cacheDuration)
    {
        _innerRepository = innerRepository;
    }

    public override async Task<PerformanceEvaluation?> UpdateAsync(PerformanceEvaluation entity)
    {
        var result = await base.UpdateAsync(entity);
        if (result != null)
        {
            InvalidateItemCache(result.EvalId);
            // Also invalidate custom list caches
            _cache.Remove($"PerformanceEvaluation_GetByEmployeeId_{result.EmployeeId}");
            _cache.Remove($"PerformanceEvaluation_GetByCycleId_{result.CycleId}");
        }
        return result;
    }

    public async Task<IEnumerable<PerformanceEvaluation>> GetByEmployeeIdAsync(int employeeId)
    {
        string key = $"PerformanceEvaluation_GetByEmployeeId_{employeeId}";
        if (!_cache.TryGetValue(key, out IEnumerable<PerformanceEvaluation>? entities))
        {
            entities = await _innerRepository.GetByEmployeeIdAsync(employeeId);
            _cache.Set(key, entities, _cacheDuration);
        }
        return entities ?? new List<PerformanceEvaluation>();
    }

    public async Task<IEnumerable<PerformanceEvaluation>> GetByCycleIdAsync(int cycleId)
    {
        string key = $"PerformanceEvaluation_GetByCycleId_{cycleId}";
        if (!_cache.TryGetValue(key, out IEnumerable<PerformanceEvaluation>? entities))
        {
            entities = await _innerRepository.GetByCycleIdAsync(cycleId);
            _cache.Set(key, entities, _cacheDuration);
        }
        return entities ?? new List<PerformanceEvaluation>();
    }
}
