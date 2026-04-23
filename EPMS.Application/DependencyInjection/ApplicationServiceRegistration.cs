using EPMS.Application.Interfaces;
using EPMS.Application.Mappings;
using EPMS.Application.Services;
using EPMS.Application.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EPMS.Application.DependencyInjection;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MeetingSettings>(configuration.GetSection("MeetingSettings"));

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

        // Org & Security Services
        services.AddScoped<IDepartmentService, DepartmentService>();
        services.AddScoped<ITeamService, TeamService>();
        services.AddScoped<IPermissionService, PermissionService>();

        // Employee & Personnel Services
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<ILevelService, LevelService>();
        services.AddScoped<IPositionService, PositionService>();
        services.AddScoped<ITokenService, TokenService>();

        return services;
    }
}
