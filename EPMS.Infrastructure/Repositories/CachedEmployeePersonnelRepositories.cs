using EPMS.Domain.Entities;
using EPMS.Domain.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace EPMS.Infrastructure.Repositories;

public class CachedEmployeeRepository : CachedBaseRepository<Employee, int>, IEmployeeRepository
{
    private readonly IEmployeeRepository _innerRepository;

    public CachedEmployeeRepository(IEmployeeRepository innerRepository, IMemoryCache cache, TimeSpan cacheDuration)
        : base(innerRepository, cache, cacheDuration)
    {
        _innerRepository = innerRepository;
    }

    public async Task<IEnumerable<Employee>> GetEmployeesByDepartmentAsync(int departmentId)
    {
        string key = $"{_cacheKeyPrefix}_Dept_{departmentId}";
        if (!_cache.TryGetValue(key, out IEnumerable<Employee>? entities))
        {
            entities = await _innerRepository.GetEmployeesByDepartmentAsync(departmentId);
            _cache.Set(key, entities, _cacheDuration);
        }
        return entities ?? new List<Employee>();
    }

    public async Task<IEnumerable<Employee>> GetDirectReportsAsync(int managerId)
    {
        string key = $"{_cacheKeyPrefix}_Reports_{managerId}";
        if (!_cache.TryGetValue(key, out IEnumerable<Employee>? entities))
        {
            entities = await _innerRepository.GetDirectReportsAsync(managerId);
            _cache.Set(key, entities, _cacheDuration);
        }
        return entities ?? new List<Employee>();
    }

    public async Task<Employee?> GetByCodeAsync(string employeeCode)
    {
        string key = $"{_cacheKeyPrefix}_Code_{employeeCode}";
        if (!_cache.TryGetValue(key, out Employee? entity))
        {
            entity = await _innerRepository.GetByCodeAsync(employeeCode);
            if (entity != null)
                _cache.Set(key, entity, _cacheDuration);
        }
        return entity;
    }
}

public class CachedLevelRepository : CachedBaseRepository<Level, string>, ILevelRepository
{
    public CachedLevelRepository(ILevelRepository innerRepository, IMemoryCache cache, TimeSpan cacheDuration)
        : base(innerRepository, cache, cacheDuration) { }
}

public class CachedPositionRepository : CachedBaseRepository<Position, int>, IPositionRepository
{
    private readonly IPositionRepository _innerRepository;

    public CachedPositionRepository(IPositionRepository innerRepository, IMemoryCache cache, TimeSpan cacheDuration)
        : base(innerRepository, cache, cacheDuration)
    {
        _innerRepository = innerRepository;
    }

    public async Task<IEnumerable<Position>> GetPositionsByLevelAsync(string levelId)
    {
        string key = $"{_cacheKeyPrefix}_Level_{levelId}";
        if (!_cache.TryGetValue(key, out IEnumerable<Position>? entities))
        {
            entities = await _innerRepository.GetPositionsByLevelAsync(levelId);
            _cache.Set(key, entities, _cacheDuration);
        }
        return entities ?? new List<Position>();
    }
}
