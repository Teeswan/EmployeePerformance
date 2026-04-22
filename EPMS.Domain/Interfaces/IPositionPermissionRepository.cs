using EPMS.Domain.Entities;

namespace EPMS.Domain.Interfaces;

public interface IPositionPermissionRepository : IBaseRepository<PositionPermission, int>
{
    Task<IEnumerable<Permission>> GetPermissionsByPositionAsync(int positionId);
    Task<PositionPermission?> GetByPositionAndPermissionAsync(int positionId, int permissionId);
}
