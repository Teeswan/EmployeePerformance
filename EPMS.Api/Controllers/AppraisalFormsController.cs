using EPMS.Application.Interfaces;
using EPMS.Shared.DTOs;
using EPMS.Shared.Requests;
using Microsoft.AspNetCore.Mvc;

namespace EPMS.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppraisalFormsController : ControllerBase
{
    private readonly IAppraisalFormService _service;

    public AppraisalFormsController(IAppraisalFormService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppraisalFormDto>>> GetAll()
    {
        var result = await _service.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AppraisalFormDto>> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<AppraisalFormDto>> Create(CreateAppraisalFormRequest request)
    {
        var result = await _service.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = result.FormId }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<AppraisalFormDto>> Update(int id, UpdateAppraisalFormRequest request)
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
