using System.Collections.Generic;
using System.Threading.Tasks;
using EPMS.Domain.Entities;
using EPMS.Domain.Interfaces;
using EPMS.Infrastructure.Contexts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using static EPMS.Infrastructure.StoredProcedures;

namespace EPMS.Infrastructure.Repositories;

public class PositionPermissionRepository : BaseRepository<PositionPermission, int>, IPositionPermissionRepository
{
    private readonly ISqlRepository<Permission, int> _sqlPermissionRepository;

    public PositionPermissionRepository(AppDbContext context, ISqlRepository<Permission, int> sqlPermissionRepository) : base(context)
    {
        _sqlPermissionRepository = sqlPermissionRepository;
    }

    public async Task<IEnumerable<Permission>> GetPermissionsByPositionAsync(int positionId)
    {
        var parameters = new[] { new SqlParameter("@PositionId", positionId) };
        return await _sqlPermissionRepository.FromSqlAsync(Permissions_GetByPosition, parameters);
    }

    public async Task<PositionPermission?> GetByPositionAndPermissionAsync(int positionId, int permissionId)
    {
        return await _context.PositionPermissions
            .FirstOrDefaultAsync(pp => pp.PositionId == positionId && pp.PermissionId == permissionId);
    }

    public override async Task<IEnumerable<PositionPermission>> GetAllAsync()
    {
        return await _dbSet.Where(pp => pp.IsActive == true).AsNoTracking().ToListAsync();
    }
}
