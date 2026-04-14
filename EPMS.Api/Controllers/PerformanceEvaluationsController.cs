using EPMS.Application.Interfaces;
using EPMS.Shared.DTOs;
using EPMS.Shared.Requests;
using Microsoft.AspNetCore.Mvc;

namespace EPMS.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PerformanceEvaluationsController : ControllerBase
{
    private readonly IPerformanceEvaluationService _service;

    public PerformanceEvaluationsController(IPerformanceEvaluationService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PerformanceEvaluationDto>>> GetAll()
    {
        var result = await _service.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PerformanceEvaluationDto>> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpGet("by-employee/{employeeId}")]
    public async Task<ActionResult<IEnumerable<PerformanceEvaluationDto>>> GetByEmployeeId(int employeeId)
    {
        var result = await _service.GetByEmployeeIdAsync(employeeId);
        return Ok(result);
    }

    [HttpGet("by-cycle/{cycleId}")]
    public async Task<ActionResult<IEnumerable<PerformanceEvaluationDto>>> GetByCycleId(int cycleId)
    {
        var result = await _service.GetByCycleIdAsync(cycleId);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<PerformanceEvaluationDto>> Create(CreatePerformanceEvaluationRequest request)
    {
        var result = await _service.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = result.EvalId }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<PerformanceEvaluationDto>> Update(int id, UpdatePerformanceEvaluationRequest request)
    {
        var result = await _service.UpdateAsync(id, request);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}
