using System.Collections.Generic;
using System.Threading.Tasks;
using EPMS.Domain.Entities;
using EPMS.Domain.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace EPMS.Infrastructure.Repositories;

public class CachedPerformanceOutcomeRepository : CachedBaseRepository<PerformanceOutcome, int>, IPerformanceOutcomeRepository
{
    private readonly IPerformanceOutcomeRepository _innerRepository;
    private readonly IMemoryCache _cache;

    public CachedPerformanceOutcomeRepository(IPerformanceOutcomeRepository innerRepository, IMemoryCache cache, TimeSpan cacheDuration) 
        : base(innerRepository, cache, cacheDuration)
    {
        _innerRepository = innerRepository;
        _cache = cache;
    }

    public override async Task<PerformanceOutcome?> UpdateAsync(PerformanceOutcome entity)
    {
        var result = await base.UpdateAsync(entity);
        if (result != null)
        {
            InvalidateItemCache(result.OutcomeId);
            // Also invalidate custom list caches
            _cache.Remove($"PerformanceOutcome_GetByEmployeeId_{result.EmployeeId}");
            _cache.Remove($"PerformanceOutcome_GetByCycleId_{result.CycleId}");
        }
        return result;
    }

    public async Task<IEnumerable<PerformanceOutcome>> GetByEmployeeIdAsync(int employeeId)
    {
        string key = $"PerformanceOutcome_GetByEmployeeId_{employeeId}";
        if (!_cache.TryGetValue(key, out IEnumerable<PerformanceOutcome>? entities))
        {
            entities = await _innerRepository.GetByEmployeeIdAsync(employeeId);
            _cache.Set(key, entities, _cacheDuration);
        }
        return entities ?? new List<PerformanceOutcome>();
    }

    public async Task<IEnumerable<PerformanceOutcome>> GetByCycleIdAsync(int cycleId)
    {
        string key = $"PerformanceOutcome_GetByCycleId_{cycleId}";
        if (!_cache.TryGetValue(key, out IEnumerable<PerformanceOutcome>? entities))
        {
            entities = await _innerRepository.GetByCycleIdAsync(cycleId);
            _cache.Set(key, entities, _cacheDuration);
        }
        return entities ?? new List<PerformanceOutcome>();
    }
}
