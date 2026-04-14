using EPMS.Application.Interfaces;
using EPMS.Domain.Interfaces;
using EPMS.Infrastructure;
using EPMS.Shared.DTOs;
using EPMS.Shared.Requests;

namespace EPMS.Application.Services;

public class FormQuestionService : IFormQuestionService
{
    private readonly IFormQuestionRepository _repository;

    public FormQuestionService(IFormQuestionRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<FormQuestionDto>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return entities.Select(MapToDto);
    }

    public async Task<IEnumerable<FormQuestionDto>> GetByFormIdAsync(int formId)
    {
        var entities = await _repository.GetByFormIdAsync(formId);
        return entities.Select(MapToDto);
    }

    public async Task<FormQuestionDto?> GetByFormAndQuestionIdAsync(int formId, int questionId)
    {
        var entity = await _repository.GetByFormAndQuestionIdAsync(formId, questionId);
        return entity == null ? null : MapToDto(entity);
    }

    public async Task<FormQuestionDto> CreateAsync(CreateFormQuestionRequest request)
    {
        var entity = new FormQuestion
        {
            FormId = request.FormId,
            QuestionId = request.QuestionId,
            SortOrder = request.SortOrder
        };

        var created = await _repository.CreateAsync(entity);
        return MapToDto(created);
    }

    public async Task<FormQuestionDto?> UpdateAsync(int formId, int questionId, UpdateFormQuestionRequest request)
    {
        var entity = new FormQuestion
        {
            FormId = formId,
            QuestionId = questionId,
            SortOrder = request.SortOrder
        };

        var updated = await _repository.UpdateAsync(entity);
        return updated == null ? null : MapToDto(updated);
    }

    public async Task<bool> DeleteAsync(int formId, int questionId)
    {
        return await _repository.DeleteAsync(formId, questionId);
    }

    private static FormQuestionDto MapToDto(FormQuestion entity)
    {
        return new FormQuestionDto
        {
            FormId = entity.FormId,
            QuestionId = entity.QuestionId,
            SortOrder = entity.SortOrder
        };
    }
}
