using EPMS.Application.Interfaces;
using EPMS.Shared.DTOs;
using EPMS.Shared.Requests;
using Microsoft.AspNetCore.Mvc;

namespace EPMS.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FormQuestionsController : ControllerBase
{
    private readonly IFormQuestionService _service;

    public FormQuestionsController(IFormQuestionService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<FormQuestionDto>>> GetAll()
    {
        var result = await _service.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("by-form/{formId}")]
    public async Task<ActionResult<IEnumerable<FormQuestionDto>>> GetByFormId(int formId)
    {
        var result = await _service.GetByFormIdAsync(formId);
        return Ok(result);
    }

    [HttpGet("{formId}/{questionId}")]
    public async Task<ActionResult<FormQuestionDto>> GetByFormAndQuestionId(int formId, int questionId)
    {
        var result = await _service.GetByFormAndQuestionIdAsync(formId, questionId);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<FormQuestionDto>> Create(CreateFormQuestionRequest request)
    {
        var result = await _service.CreateAsync(request);
        return CreatedAtAction(nameof(GetByFormAndQuestionId),
            new { formId = result.FormId, questionId = result.QuestionId }, result);
    }

    [HttpPut("{formId}/{questionId}")]
    public async Task<ActionResult<FormQuestionDto>> Update(int formId, int questionId, UpdateFormQuestionRequest request)
    {
        var result = await _service.UpdateAsync(formId, questionId, request);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{formId}/{questionId}")]
    public async Task<IActionResult> Delete(int formId, int questionId)
    {
        var deleted = await _service.DeleteAsync(formId, questionId);
        if (!deleted) return NotFound();
        return NoContent();
    }
}
