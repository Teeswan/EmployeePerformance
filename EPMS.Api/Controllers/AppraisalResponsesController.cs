using EPMS.Application.Interfaces;
using EPMS.Shared.DTOs;
using EPMS.Shared.Requests;
using Microsoft.AspNetCore.Mvc;

namespace EPMS.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppraisalResponsesController : ControllerBase
{
    private readonly IAppraisalResponseService _service;

    public AppraisalResponsesController(IAppraisalResponseService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppraisalResponseDto>>> GetAll()
    {
        var result = await _service.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AppraisalResponseDto>> GetById(long id)
    {
        var result = await _service.GetByIdAsync(id);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpGet("by-evaluation/{evalId}")]
    public async Task<ActionResult<IEnumerable<AppraisalResponseDto>>> GetByEvalId(int evalId)
    {
        var result = await _service.GetByEvalIdAsync(evalId);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<AppraisalResponseDto>> Create(CreateAppraisalResponseRequest request)
    {
        var result = await _service.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = result.ResponseId }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<AppraisalResponseDto>> Update(long id, UpdateAppraisalResponseRequest request)
    {
        var result = await _service.UpdateAsync(id, request);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}
