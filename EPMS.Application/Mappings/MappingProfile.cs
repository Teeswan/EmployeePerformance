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

        CreateMap<MeetingNote, NoteDto>().ReverseMap();

        CreateMap<OneOnOneMeeting, MeetingDto>()
            .ForMember(dest => dest.MeetingId, opt => opt.MapFrom(src => src.MeetingId))
            .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.ScheduledDateTime))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.MeetingStatus))
            .ForMember(dest => dest.EndTime, opt => opt.MapFrom((src, dest, destMember, context) =>
            {
                var duration = context.Items.ContainsKey("StandardDuration") ? (int)context.Items["StandardDuration"] : 45;
                return src.ScheduledDateTime.AddMinutes(duration);
            }))
            .ForMember(dest => dest.CanJoin, opt => opt.MapFrom((src, dest, destMember, context) =>
            {
                var buffer = context.Items.ContainsKey("JoinBufferMinutes") ? (int)context.Items["JoinBufferMinutes"] : 0;
                var duration = context.Items.ContainsKey("StandardDuration") ? (int)context.Items["StandardDuration"] : 45;
                var now = DateTime.UtcNow;
                var endTime = src.ScheduledDateTime.AddMinutes(duration);
          
                bool isWithinBuffer = now >= src.ScheduledDateTime.AddMinutes(-buffer) && now <= endTime;

                bool isManagerAlreadyIn = src.MeetingStatus == "Started";

                return isWithinBuffer || isManagerAlreadyIn;
            }));

        // Add more mappings as needed for other entities
    }
}
