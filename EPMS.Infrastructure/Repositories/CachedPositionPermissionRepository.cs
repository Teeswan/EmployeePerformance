using System.Collections.Generic;
using System.Threading.Tasks;
using EPMS.Domain.Entities;
using EPMS.Domain.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace EPMS.Infrastructure.Repositories;

public class CachedPositionPermissionRepository : CachedBaseRepository<PositionPermission, int>, IPositionPermissionRepository
{
    private readonly IPositionPermissionRepository _innerRepository;

    public CachedPositionPermissionRepository(IPositionPermissionRepository innerRepository, IMemoryCache cache, TimeSpan cacheDuration) 
        : base(innerRepository, cache, cacheDuration)
    {
        _innerRepository = innerRepository;
    }

    public async Task<IEnumerable<Permission>> GetPermissionsByPositionAsync(int positionId)
    {
        string key = $"{_cacheKeyPrefix}_GetByPosition_{positionId}";
        if (!_cache.TryGetValue(key, out IEnumerable<Permission>? entities))
        {
            entities = await _innerRepository.GetPermissionsByPositionAsync(positionId);
            _cache.Set(key, entities, new MemoryCacheEntryOptions().SetAbsoluteExpiration(_cacheDuration));
        }
        return entities ?? new List<Permission>();
    }

    public async Task<PositionPermission?> GetByPositionAndPermissionAsync(int positionId, int permissionId)
    {
        string key = $"{_cacheKeyPrefix}_GetByPosition_{positionId}_Permission_{permissionId}";
        if (!_cache.TryGetValue(key, out PositionPermission? entity))
        {
            entity = await _innerRepository.GetByPositionAndPermissionAsync(positionId, permissionId);
            if (entity != null)
                _cache.Set(key, entity, new MemoryCacheEntryOptions().SetAbsoluteExpiration(_cacheDuration));
        }
        return entity;
    }

    protected override void InvalidateItemCache(int id)
    {
        base.InvalidateItemCache(id);
        // We don't have the positionId easily here, but we can clear GetAll
        base.InvalidateCache();
    }

    protected override void InvalidateCache()
    {
        base.InvalidateCache();
        // Clear all permissions related caches for positions
        // This is tricky without a key tracking system, so we might just clear what we can.
    }
}
