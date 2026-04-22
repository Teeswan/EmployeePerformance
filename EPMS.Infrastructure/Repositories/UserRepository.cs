using EPMS.Domain.Entities;
using EPMS.Domain.Interfaces;
using EPMS.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EPMS.Infrastructure.Repositories;

public class UserRepository : BaseRepository<User, int>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await _context.Users
            .Include(u => u.Employee)
            .FirstOrDefaultAsync(u => u.Username == username);
    }
}
