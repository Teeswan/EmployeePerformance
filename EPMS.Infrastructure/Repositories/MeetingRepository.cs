// EPMS.Infrastructure/Repositories/MeetingRepository.cs
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EPMS.Domain.Entities;
using EPMS.Domain.Interfaces;
using EPMS.Infrastructure.Contexts;

namespace EPMS.Infrastructure.Repositories;

public class MeetingRepository : IMeetingRepository
{
    private readonly AppDbContext _context;

    public MeetingRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<OneOnOneMeeting?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.OneOnOneMeetings.FirstOrDefaultAsync(m => m.MeetingId == id, cancellationToken);
    }

    public async Task<IEnumerable<OneOnOneMeeting>> GetHistoryTimelineAsync(int rootMeetingId, CancellationToken cancellationToken)
    {
        return await _context.OneOnOneMeetings
            .Where(m => m.MeetingId == rootMeetingId || m.ParentMeetingId == rootMeetingId)
            .OrderBy(m => m.ScheduledDateTime)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<OneOnOneMeeting>> GetManagerDashboardAsync(int managerId, CancellationToken cancellationToken)
    {
        return await _context.OneOnOneMeetings
            .Where(m => m.ManagerId == managerId)
            .OrderBy(m => m.ScheduledDateTime)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<OneOnOneMeeting>> GetEmployeeDashboardAsync(int employeeId, CancellationToken cancellationToken)
    {
        return await _context.OneOnOneMeetings
            .Where(m => m.EmployeeId == employeeId)
            .OrderBy(m => m.ScheduledDateTime)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(OneOnOneMeeting meeting, CancellationToken cancellationToken)
    {
        await _context.OneOnOneMeetings.AddAsync(meeting, cancellationToken);
    }
}
// Note: UnitOfWork and MeetingHub remain exactly the same as previously written!