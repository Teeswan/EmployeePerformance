using EPMS.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPMS.Shared.DTOs;
using EPMS.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EPMS.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly AppDbContext _context;

        // Constructor Injection သုံးပြီး DbContext ကို လှမ်းခေါ်ခြင်း
        public RoleService(AppDbContext context)
        {
            _context = context;
        }

        // Database ထဲက Role တွေကို DTO အဖြစ်ပြောင်းပြီး ဆွဲထုတ်ခြင်း
        public async Task<List<RoleDto>> GetAllRolesAsync()
        {
            return await _context.Roles
                .Select(r => new RoleDto
                {
                    RoleID = r.RoleId,
                    RoleName = r.RoleName
                })
                .ToListAsync();
        }

    }
}
