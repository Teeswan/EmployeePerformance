using EPMS.Domain.Entities;

namespace EPMS.Domain.Interfaces;

public interface IUserRepository : IBaseRepository<User, int>
{
    Task<User?> GetByUsernameAsync(string username);
}
