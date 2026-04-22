using EPMS.Application.DependencyInjection;
using EPMS.Infrastructure.DependencyInjection;
using EPMS.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// 1. Define the CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazor", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 2. Enable the Policy (Order matters: Place before MapControllers)
app.UseCors("AllowBlazor");

// app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.InitializeDatabase();
}

app.Run();