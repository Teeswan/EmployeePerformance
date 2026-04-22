using EPMS.Application.Interfaces;
using EPMS.Domain.Interfaces;
using EPMS.Domain.Entities;
using EPMS.Shared.DTOs;
using EPMS.Shared.Requests;

namespace EPMS.Application.Services;

public class AppraisalFormService : IAppraisalFormService
{
    private readonly IAppraisalFormRepository _repository;

    public AppraisalFormService(IAppraisalFormRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<AppraisalFormDto>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return entities.Select(MapToDto);
    }

    public async Task<AppraisalFormDto?> GetByIdAsync(int formId)
    {
        var entity = await _repository.GetByIdAsync(formId);
        return entity == null ? null : MapToDto(entity);
    }

    public async Task<AppraisalFormDto> CreateAsync(CreateAppraisalFormRequest request)
    {
        var entity = new ApplicationForm
        {
            FormName = request.FormName,
            IsActive = request.IsActive
        };

        var created = await _repository.CreateAsync(entity);
        return MapToDto(created);
    }

    public async Task<AppraisalFormDto?> UpdateAsync(int formId, UpdateAppraisalFormRequest request)
    {
        var entity = new ApplicationForm
        {
            FormId = formId,
            FormName = request.FormName,
            IsActive = request.IsActive
        };

        var updated = await _repository.UpdateAsync(entity);
        return updated == null ? null : MapToDto(updated);
    }

    public async Task<bool> DeleteAsync(int formId)
    {
        return await _repository.DeleteAsync(formId);
    }

    private static AppraisalFormDto MapToDto(ApplicationForm entity)
    {
        return new AppraisalFormDto
        {
            FormId = entity.FormId,
            FormName = entity.FormName,
            IsActive = entity.IsActive
        };
    }
}
