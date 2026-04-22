using System.Collections.Generic;
using System.Threading.Tasks;
using EPMS.Domain.Entities;
using EPMS.Domain.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace EPMS.Infrastructure.Repositories;

public class CachedAppraisalResponseRepository : CachedBaseRepository<AppraisalResponse, long>, IAppraisalResponseRepository
{
    private readonly IAppraisalResponseRepository _innerRepository;
    private readonly IMemoryCache _cache;

    public CachedAppraisalResponseRepository(IAppraisalResponseRepository innerRepository, IMemoryCache cache, TimeSpan cacheDuration) 
        : base(innerRepository, cache, cacheDuration)
    {
        _innerRepository = innerRepository;
        _cache = cache;
    }

    public override async Task<AppraisalResponse?> UpdateAsync(AppraisalResponse entity)
    {
        var result = await base.UpdateAsync(entity);
        if (result != null)
        {
            InvalidateItemCache(result.ResponseId);
            // Also invalidate custom list caches
            _cache.Remove($"AppraisalResponse_GetByEvalId_{result.EvalId}");
        }
        return result;
    }

    public async Task<IEnumerable<AppraisalResponse>> GetByEvalIdAsync(int evalId)
    {
        string key = $"AppraisalResponse_GetByEvalId_{evalId}";
        if (!_cache.TryGetValue(key, out IEnumerable<AppraisalResponse>? entities))
        {
            entities = await _innerRepository.GetByEvalIdAsync(evalId);
            _cache.Set(key, entities, _cacheDuration);
        }
        return entities ?? new List<AppraisalResponse>();
    }
}
