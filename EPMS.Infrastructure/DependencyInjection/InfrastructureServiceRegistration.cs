using EPMS.Domain.Entities;
using EPMS.Domain.Interfaces;
using EPMS.Infrastructure.Contexts;
using EPMS.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EPMS.Infrastructure.DependencyInjection;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMemoryCache();

        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        var defaultCacheDurationMinutes = configuration.GetValue<int?>("CacheSettings:DefaultCacheDurationMinutes") ?? 10;
        var defaultCacheDuration = TimeSpan.FromMinutes(defaultCacheDurationMinutes);

        // Appraisal Cycle Repository with Caching
        services.AddScoped<IAppraisalCycleRepository, AppraisalCycleRepository>();
        services.Decorate<IAppraisalCycleRepository>((inner, provider) =>
            new CachedAppraisalCycleRepository(inner, provider.GetRequiredService<IMemoryCache>(), defaultCacheDuration));

        // Appraisal Response Repository with Caching
        services.AddScoped<IAppraisalResponseRepository, AppraisalResponseRepository>();
        services.Decorate<IAppraisalResponseRepository>((inner, provider) =>
            new CachedAppraisalResponseRepository(inner, provider.GetRequiredService<IMemoryCache>(), defaultCacheDuration));

        // Appraisal Question Repository with Caching
        services.AddScoped<IAppraisalQuestionRepository, AppraisalQuestionRepository>();
        services.Decorate<IAppraisalQuestionRepository>((inner, provider) =>
            new CachedAppraisalQuestionRepository(inner, provider.GetRequiredService<IMemoryCache>(), defaultCacheDuration));

        // Appraisal Form Repository with Caching
        services.AddScoped<IAppraisalFormRepository, ApplicationFormRepository>();
        services.Decorate<IAppraisalFormRepository>((inner, provider) =>
            new CachedAppraisalFormRepository(inner, provider.GetRequiredService<IMemoryCache>(), defaultCacheDuration));

        services.AddScoped<IFormQuestionRepository>(provider =>
            new FormQuestionRepository(
                provider.GetRequiredService<AppDbContext>(),
                provider.GetRequiredService<ISqlRepository<FormQuestion, object[]>>()));

        services.AddScoped(typeof(ISqlRepository<,>), typeof(SqlRepository<,>));



        // Performance Evaluation Repository with Caching
        services.AddScoped<IPerformanceEvaluationRepository, PerformanceEvaluationRepository>();
        services.Decorate<IPerformanceEvaluationRepository>((inner, provider) =>
            new CachedPerformanceEvaluationRepository(inner, provider.GetRequiredService<IMemoryCache>(), defaultCacheDuration));

        // Performance Outcome Repository with Caching
        services.AddScoped<IPerformanceOutcomeRepository, PerformanceOutcomeRepository>();
        services.Decorate<IPerformanceOutcomeRepository>((inner, provider) =>
            new CachedPerformanceOutcomeRepository(inner, provider.GetRequiredService<IMemoryCache>(), defaultCacheDuration));

        return services;
    }
}
