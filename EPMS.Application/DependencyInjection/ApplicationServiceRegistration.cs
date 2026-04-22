using EPMS.Application.Interfaces;
using EPMS.Application.Mappings;
using EPMS.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EPMS.Application.DependencyInjection;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());

        services.AddScoped<IAppraisalCycleService, AppraisalCycleService>();
        services.AddScoped<IAppraisalQuestionService, AppraisalQuestionService>();
        services.AddScoped<IAppraisalResponseService, AppraisalResponseService>();
        services.AddScoped<IAppraisalFormService, AppraisalFormService>();
        services.AddScoped<IFormQuestionService, FormQuestionService>();
        services.AddScoped<IPerformanceEvaluationService, PerformanceEvaluationService>();
        services.AddScoped<IPerformanceOutcomeService, PerformanceOutcomeService>();
        services.AddScoped<IExcelPdfService, ExcelPdfService>();

        services.AddScoped<IMeetingService, MeetingService>();

        return services;
    }
}
