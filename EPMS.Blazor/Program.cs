using EPMS.Blazor;
using EPMS.Blazor.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Updated to use HTTP port to avoid certificate issues in development
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("http://localhost:5111/")
});

builder.Services.AddScoped<IAppraisalCycleBlazorService, AppraisalCycleBlazorService>();
builder.Services.AddScoped<IAppraisalFormBlazorService, AppraisalFormBlazorService>();
builder.Services.AddScoped<IAppraisalQuestionBlazorService, AppraisalQuestionBlazorService>();
builder.Services.AddScoped<IAppraisalResponseBlazorService, AppraisalResponseBlazorService>();
builder.Services.AddScoped<IPerformanceEvaluationBlazorService, PerformanceEvaluationBlazorService>();
builder.Services.AddScoped<IPerformanceOutcomeBlazorService, PerformanceOutcomeBlazorService>();
builder.Services.AddScoped<IFormQuestionBlazorService, FormQuestionBlazorService>();

await builder.Build().RunAsync();