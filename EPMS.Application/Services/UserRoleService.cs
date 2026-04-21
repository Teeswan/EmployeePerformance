using EPMS.Application.Interfaces;
using EPMS.Infrastructure.Contexts;
using EPMS.Shared.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPMS.Application.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly AppDbContext _context;

        public UserRoleService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserRoleDto>> GetAllUserRolesAsync()
        {
            return await _context.UserRoles
                .Select(ur => new UserRoleDto
                {
                    UserId = ur.UserId,
                    RoleId = ur.RoleId
                })
                .ToListAsync();
        }
    }
}
