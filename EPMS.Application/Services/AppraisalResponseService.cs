using EPMS.Application.Interfaces;
using EPMS.Domain.Interfaces;
using EPMS.Infrastructure;
using EPMS.Shared.DTOs;
using EPMS.Shared.Requests;

namespace EPMS.Application.Services;

public class AppraisalResponseService : IAppraisalResponseService
{
    private readonly IAppraisalResponseRepository _repository;

    public AppraisalResponseService(IAppraisalResponseRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<AppraisalResponseDto>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return entities.Select(MapToDto);
    }

    public async Task<AppraisalResponseDto?> GetByIdAsync(long responseId)
    {
        var entity = await _repository.GetByIdAsync(responseId);
        return entity == null ? null : MapToDto(entity);
    }

    public async Task<IEnumerable<AppraisalResponseDto>> GetByEvalIdAsync(int evalId)
    {
        var entities = await _repository.GetByEvalIdAsync(evalId);
        return entities.Select(MapToDto);
    }

    public async Task<AppraisalResponseDto> CreateAsync(CreateAppraisalResponseRequest request)
    {
        var entity = new AppraisalResponse
        {
            EvalId = request.EvalId,
            QuestionId = request.QuestionId,
            RespondentId = request.RespondentId,
            AnswerText = request.AnswerText,
            RatingValue = request.RatingValue,
            IsAnonymous = request.IsAnonymous
        };

        var created = await _repository.CreateAsync(entity);
        return MapToDto(created);
    }

    public async Task<AppraisalResponseDto?> UpdateAsync(long responseId, UpdateAppraisalResponseRequest request)
    {
        var entity = new AppraisalResponse
        {
            ResponseId = responseId,
            EvalId = request.EvalId,
            QuestionId = request.QuestionId,
            RespondentId = request.RespondentId,
            AnswerText = request.AnswerText,
            RatingValue = request.RatingValue,
            IsAnonymous = request.IsAnonymous
        };

        var updated = await _repository.UpdateAsync(entity);
        return updated == null ? null : MapToDto(updated);
    }

    public async Task<bool> DeleteAsync(long responseId)
    {
        return await _repository.DeleteAsync(responseId);
    }

    private static AppraisalResponseDto MapToDto(AppraisalResponse entity)
    {
        return new AppraisalResponseDto
        {
            ResponseId = entity.ResponseId,
            EvalId = entity.EvalId,
            QuestionId = entity.QuestionId,
            RespondentId = entity.RespondentId,
            AnswerText = entity.AnswerText,
            RatingValue = entity.RatingValue,
            IsAnonymous = entity.IsAnonymous
        };
    }
}
