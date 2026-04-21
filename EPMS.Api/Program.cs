using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
///



builder.Services.AddDbContext<EPMS.Infrastructure.Contexts.AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<EPMS.Application.Interfaces.IRoleService, EPMS.Application.Services.RoleService>();

builder.Services.AddScoped<EPMS.Application.Interfaces.IEmployeeService, EPMS.Application.Services.EmployeeService>();

builder.Services.AddScoped<EPMS.Application.Interfaces.IUserService, EPMS.Application.Services.UserService>();

builder.Services.AddScoped<EPMS.Application.Interfaces.INotificationService, EPMS.Application.Services.NotificationService>();

builder.Services.AddScoped<EPMS.Application.Interfaces.IUserRoleService, EPMS.Application.Services.UserRoleService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "EPMS API V1");
        c.RoutePrefix = string.Empty; 
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
