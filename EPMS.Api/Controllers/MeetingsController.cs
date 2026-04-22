using System.Threading;
using System.Threading.Tasks;
using EPMS.Application.Interfaces;
using EPMS.Infrastructure.Authorization;
using EPMS.Shared.Constants;
using EPMS.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace EPMS.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MeetingsController : ControllerBase
{
    private readonly IMeetingService _meetingService;

    public MeetingsController(IMeetingService meetingService) => _meetingService = meetingService;

    [HttpPost("schedule-bulk")]
    [HasPermission(Permissions.Meetings.Manage)]
    public async Task<IActionResult> ScheduleBulk([FromBody] BulkScheduleDto request, CancellationToken ct)
    {
        // Resolving Manager ID from Token
        var userIdClaim = User.FindFirst("UserId")?.Value;
        if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int managerId))
        {
            return Unauthorized();
        }
        
        await _meetingService.ScheduleBulkMeetingsAsync(managerId, request, ct);
        return Ok(new { message = "All meetings scheduled successfully." });
    }

    [HttpGet("manager-dashboard/{managerId}")]
    [HasPermission(Permissions.Meetings.View)]
    public async Task<IActionResult> GetManagerDashboard(int managerId, CancellationToken ct)
    {
        var meetings = await _meetingService.GetManagerDashboardAsync(managerId, ct);
        return Ok(meetings);
    }
}