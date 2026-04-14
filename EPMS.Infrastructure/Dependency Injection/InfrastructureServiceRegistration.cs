using EPMS.Domain.Interfaces;
using EPMS.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EPMS.Infrastructure.DependencyInjection;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IAppraisalCycleRepository, AppraisalCycleRepository>();
        services.AddScoped<IAppraisalQuestionRepository, AppraisalQuestionRepository>();
        services.AddScoped<IAppraisalResponseRepository, AppraisalResponseRepository>();
        services.AddScoped<IAppraisalFormRepository, AppraisalFormRepository>();
        services.AddScoped<IFormQuestionRepository, FormQuestionRepository>();
        services.AddScoped<IPerformanceEvaluationRepository, PerformanceEvaluationRepository>();
        services.AddScoped<IPerformanceOutcomeRepository, PerformanceOutcomeRepository>();

        return services;
    }
}
