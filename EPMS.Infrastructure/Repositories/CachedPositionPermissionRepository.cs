using System.Collections.Generic;
using System.Threading.Tasks;
using EPMS.Domain.Entities;
using EPMS.Domain.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace EPMS.Infrastructure.Repositories;

public class CachedPositionPermissionRepository : IPositionPermissionRepository
{
    private readonly IPositionPermissionRepository _innerRepository;
    private readonly IMemoryCache _cache;
    private readonly TimeSpan _cacheDuration;
    private readonly string _cacheKeyPrefix = nameof(PositionPermission);

    public CachedPositionPermissionRepository(IPositionPermissionRepository innerRepository, IMemoryCache cache, TimeSpan cacheDuration)
    {
        _innerRepository = innerRepository;
        _cache = cache;
        _cacheDuration = cacheDuration;
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

    public async Task<PositionPermission> CreateAsync(PositionPermission entity)
    {
        var createdEntity = await _innerRepository.CreateAsync(entity);
        InvalidateCache();
        return createdEntity;
    }

    public async Task<bool> DeleteAsync(int positionId, int permissionId)
    {
        var result = await _innerRepository.DeleteAsync(positionId, permissionId);
        if (result)
        {
            InvalidateCache();
            string key = $"{_cacheKeyPrefix}_GetByPosition_{positionId}_Permission_{permissionId}";
            _cache.Remove(key);
        }
        return result;
    }

    private void InvalidateCache()
    {
        // Clear general caches if any. 
        // For PositionPermission, we might want to clear GetPermissionsByPosition caches
        // but since we don't have a list of all positionIds, we might just rely on expiration
        // or clear specific keys if we had them.
    }
}
