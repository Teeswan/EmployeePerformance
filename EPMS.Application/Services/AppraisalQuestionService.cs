using EPMS.Application.Interfaces;
using EPMS.Domain.Interfaces;
using EPMS.Infrastructure;
using EPMS.Shared.DTOs;
using EPMS.Shared.Requests;

namespace EPMS.Application.Services;

public class AppraisalQuestionService : IAppraisalQuestionService
{
    private readonly IAppraisalQuestionRepository _repository;

    public AppraisalQuestionService(IAppraisalQuestionRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<AppraisalQuestionDto>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return entities.Select(MapToDto);
    }

    public async Task<AppraisalQuestionDto?> GetByIdAsync(int questionId)
    {
        var entity = await _repository.GetByIdAsync(questionId);
        return entity == null ? null : MapToDto(entity);
    }

    public async Task<AppraisalQuestionDto> CreateAsync(CreateAppraisalQuestionRequest request)
    {
        var entity = new AppraisalQuestion
        {
            QuestionText = request.QuestionText,
            Category = request.Category,
            IsRequired = request.IsRequired
        };

        var created = await _repository.CreateAsync(entity);
        return MapToDto(created);
    }

    public async Task<AppraisalQuestionDto?> UpdateAsync(int questionId, UpdateAppraisalQuestionRequest request)
    {
        var entity = new AppraisalQuestion
        {
            QuestionId = questionId,
            QuestionText = request.QuestionText,
            Category = request.Category,
            IsRequired = request.IsRequired
        };

        var updated = await _repository.UpdateAsync(entity);
        return updated == null ? null : MapToDto(updated);
    }

    public async Task<bool> DeleteAsync(int questionId)
    {
        return await _repository.DeleteAsync(questionId);
    }

    private static AppraisalQuestionDto MapToDto(AppraisalQuestion entity)
    {
        return new AppraisalQuestionDto
        {
            QuestionId = entity.QuestionId,
            QuestionText = entity.QuestionText,
            Category = entity.Category,
            IsRequired = entity.IsRequired
        };
    }
}
