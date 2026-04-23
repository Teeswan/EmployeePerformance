using System.Collections.Generic;
using System.Threading.Tasks;
using EPMS.Domain.Entities;
using EPMS.Domain.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace EPMS.Infrastructure.Repositories;

public class CachedTeamRepository : CachedBaseRepository<Team, int>, ITeamRepository
{
    private readonly ITeamRepository _innerRepository;

    public CachedTeamRepository(ITeamRepository innerRepository, IMemoryCache cache, TimeSpan cacheDuration) 
        : base(innerRepository, cache, cacheDuration)
    {
        _innerRepository = innerRepository;
    }

    public async Task<IEnumerable<dynamic>> GetTeamsByDepartmentAsync(int departmentId)
    {
        string key = $"{_cacheKeyPrefix}_GetByDepartment_{departmentId}";
        if (!_cache.TryGetValue(key, out IEnumerable<dynamic>? entities))
        {
            entities = await _innerRepository.GetTeamsByDepartmentAsync(departmentId);
            _cache.Set(key, entities, new MemoryCacheEntryOptions().SetAbsoluteExpiration(_cacheDuration));
        }
        return entities ?? new List<dynamic>();
    }
}
