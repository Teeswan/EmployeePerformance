using System.Collections.Generic;
using System.Threading.Tasks;
using EPMS.Domain.Entities;
using EPMS.Domain.Interfaces;
using EPMS.Infrastructure.Contexts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using static EPMS.Infrastructure.StoredProcedures;

namespace EPMS.Infrastructure.Repositories;

public class PositionPermissionRepository : IPositionPermissionRepository
{
    private readonly AppDbContext _context;
    private readonly DbSet<PositionPermission> _dbSet;
    private readonly ISqlRepository<Permission, int> _sqlPermissionRepository;

    public PositionPermissionRepository(AppDbContext context, ISqlRepository<Permission, int> sqlPermissionRepository)
    {
        _context = context;
        _dbSet = _context.Set<PositionPermission>();
        _sqlPermissionRepository = sqlPermissionRepository;
    }

    public async Task<IEnumerable<Permission>> GetPermissionsByPositionAsync(int positionId)
    {
        var parameters = new[] { new SqlParameter("@PositionId", positionId) };
        return await _sqlPermissionRepository.FromSqlAsync(Permissions_GetByPosition, parameters);
    }

    public async Task<PositionPermission?> GetByPositionAndPermissionAsync(int positionId, int permissionId)
    {
        return await _dbSet
            .FirstOrDefaultAsync(pp => pp.PositionId == positionId && pp.PermissionId == permissionId);
    }

    public async Task<PositionPermission> CreateAsync(PositionPermission entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> DeleteAsync(int positionId, int permissionId)
    {
        var entity = await GetByPositionAndPermissionAsync(positionId, permissionId);
        if (entity == null) return false;

        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}
