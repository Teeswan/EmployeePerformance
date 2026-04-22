using EPMS.Application.Interfaces;
using EPMS.Infrastructure.Authorization;
using EPMS.Shared.Constants;
using EPMS.Shared.DTOs;
using EPMS.Shared.Requests;
using Microsoft.AspNetCore.Mvc;

namespace EPMS.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PermissionsController : ControllerBase
{
    private readonly IPermissionService _service;

    public PermissionsController(IPermissionService service)
    {
        _service = service;
    }

    [HttpGet]
    [HasPermission(Permissions.Security.ViewPermissions)]
    public async Task<ActionResult<IEnumerable<PermissionDto>>> GetAll()
    {
        var result = await _service.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("position/{positionId}")]
    [HasPermission(Permissions.Security.ViewPermissions)]
    public async Task<ActionResult<IEnumerable<PermissionDto>>> GetByPosition(int positionId)
    {
        var result = await _service.GetByPositionAsync(positionId);
        return Ok(result);
    }

    [HttpPost("assign")]
    [HasPermission(Permissions.Security.Manage)]
    public async Task<IActionResult> AssignPermission(AssignPermissionRequest request)
    {
        var result = await _service.AssignPermissionAsync(request);
        if (!result) return BadRequest();
        return Ok();
    }

    [HttpPost("revoke")]
    [HasPermission(Permissions.Security.Manage)]
    public async Task<IActionResult> RevokePermission(RevokePermissionRequest request)
    {
        var result = await _service.RevokePermissionAsync(request);
        if (!result) return BadRequest();
        return Ok();
    }
}
