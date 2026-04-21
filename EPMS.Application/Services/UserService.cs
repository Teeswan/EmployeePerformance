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
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            return await _context.Users
                .Include(u => u.Employee) 
                .Include(u => u.Roles)    
                .Select(u => new UserDto
                {
                    UserId = u.UserId,
                    Username = u.Username,
                    EmployeeId = (int)u.EmployeeId,
                    EmployeeName = u.Employee.FullName, 
                    Roles = u.Roles.Select(r => r.RoleName).ToList() 
                })
                .ToListAsync();
        }
    }
}
