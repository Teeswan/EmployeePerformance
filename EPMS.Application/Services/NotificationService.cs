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
    public class NotificationService : INotificationService
    {
        private readonly AppDbContext _context;

        public NotificationService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<NotificationDto>> GetAllNotificationsAsync()
        {
            return await _context.Notifications
                .Select(n => new NotificationDto
                {
                    NotificationId = (int)n.NotificationId,
                    Title = n.Title,
                    Type = n.Type,
                    RelatedEntityId = (int?)n.RelatedEntityId,
                    IsRead = n.IsRead,
                    CreatedAt = n.CreatedAt,
                    UserId = (int)n.UserId
                })
                .ToListAsync();
        }

    }
}
