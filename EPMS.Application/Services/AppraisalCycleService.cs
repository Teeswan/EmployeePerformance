using EPMS.Application.Interfaces;
using EPMS.Domain.Interfaces;
using EPMS.Infrastructure;
using EPMS.Shared.DTOs;
using EPMS.Shared.Requests;

namespace EPMS.Application.Services;

public class AppraisalCycleService : IAppraisalCycleService
{
    private readonly IAppraisalCycleRepository _repository;

    public AppraisalCycleService(IAppraisalCycleRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<AppraisalCycleDto>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return entities.Select(MapToDto);
    }

    public async Task<AppraisalCycleDto?> GetByIdAsync(int cycleId)
    {
        var entity = await _repository.GetByIdAsync(cycleId);
        return entity == null ? null : MapToDto(entity);
    }

    public async Task<AppraisalCycleDto> CreateAsync(CreateAppraisalCycleRequest request)
    {
        var entity = new AppraisalCycle
        {
            CycleName = request.CycleName,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            EvaluationPeriod = request.EvaluationPeriod,
            CycleStatus = request.CycleStatus
        };

        var created = await _repository.CreateAsync(entity);
        return MapToDto(created);
    }

    public async Task<AppraisalCycleDto?> UpdateAsync(int cycleId, UpdateAppraisalCycleRequest request)
    {
        var entity = new AppraisalCycle
        {
            CycleId = cycleId,
            CycleName = request.CycleName,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            EvaluationPeriod = request.EvaluationPeriod,
            CycleStatus = request.CycleStatus
        };

        var updated = await _repository.UpdateAsync(entity);
        return updated == null ? null : MapToDto(updated);
    }

    public async Task<bool> DeleteAsync(int cycleId)
    {
        return await _repository.DeleteAsync(cycleId);
    }

    private static AppraisalCycleDto MapToDto(AppraisalCycle entity)
    {
        return new AppraisalCycleDto
        {
            CycleId = entity.CycleId,
            CycleName = entity.CycleName,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            EvaluationPeriod = entity.EvaluationPeriod,
            CycleStatus = entity.CycleStatus
        };
    }
}
