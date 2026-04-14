using EPMS.Application.Interfaces;
using EPMS.Shared.DTOs;
using EPMS.Shared.Requests;
using Microsoft.AspNetCore.Mvc;

namespace EPMS.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PerformanceOutcomesController : ControllerBase
{
    private readonly IPerformanceOutcomeService _service;

    public PerformanceOutcomesController(IPerformanceOutcomeService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PerformanceOutcomeDto>>> GetAll()
    {
        var result = await _service.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PerformanceOutcomeDto>> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpGet("by-employee/{employeeId}")]
    public async Task<ActionResult<IEnumerable<PerformanceOutcomeDto>>> GetByEmployeeId(int employeeId)
    {
        var result = await _service.GetByEmployeeIdAsync(employeeId);
        return Ok(result);
    }

    [HttpGet("by-cycle/{cycleId}")]
    public async Task<ActionResult<IEnumerable<PerformanceOutcomeDto>>> GetByCycleId(int cycleId)
    {
        var result = await _service.GetByCycleIdAsync(cycleId);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<PerformanceOutcomeDto>> Create(CreatePerformanceOutcomeRequest request)
    {
        var result = await _service.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = result.OutcomeId }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<PerformanceOutcomeDto>> Update(int id, UpdatePerformanceOutcomeRequest request)
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
