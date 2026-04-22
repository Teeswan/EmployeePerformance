using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EPMS.Shared.DTOs;
using EPMS.Application.Interfaces;

namespace EPMS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MeetingsController : ControllerBase
{
    private readonly IMeetingService _meetingService;

    public MeetingsController(IMeetingService meetingService) => _meetingService = meetingService;

    [HttpPost("schedule-bulk")]
    public async Task<IActionResult> ScheduleBulk([FromBody] BulkScheduleDto request, CancellationToken ct)
    {
        // Resolving Manager ID from Token
        var managerId = int.Parse(User.FindFirst("UserId")?.Value ?? "1");
        await _meetingService.ScheduleBulkMeetingsAsync(managerId, request, ct);
        return Ok(new { message = "All meetings scheduled successfully." });
    }

    [HttpGet("manager-dashboard/{managerId}")]
    public async Task<IActionResult> GetManagerDashboard(int managerId, CancellationToken ct)
    {
        var meetings = await _meetingService.GetManagerDashboardAsync(managerId, ct);
        return Ok(meetings);
    }
}