using AutoMapper;
using EPMS.Domain.Entities;
using EPMS.Shared.DTOs;
using EPMS.Shared.Requests;

namespace EPMS.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AppraisalCycle, AppraisalCycleDto>();
        CreateMap<CreateAppraisalCycleRequest, AppraisalCycle>();
        CreateMap<UpdateAppraisalCycleRequest, AppraisalCycle>();

        CreateMap<FormQuestion, FormQuestionDto>();
        CreateMap<CreateFormQuestionRequest, FormQuestion>();
        CreateMap<UpdateFormQuestionRequest, FormQuestion>();
        
        // Add more mappings as needed for other entities
    }
}
