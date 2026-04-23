using EPMS.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPMS.Application.Interfaces
{
    public interface IMeetingService
    {
        Task ScheduleBulkMeetingsAsync(int managerId, BulkScheduleDto request, CancellationToken ct);
        Task<IEnumerable<MeetingDto>> GetManagerDashboardAsync(int managerId, CancellationToken ct);
        Task<NoteDto> AddNoteAsync(int meetingId, CreateMeetingNoteDto dto, int authorId, CancellationToken ct);
        Task StartMeetingAsync(int meetingId, CancellationToken ct);
        Task EndMeetingAsync(int meetingId, CancellationToken ct);
    }
}
