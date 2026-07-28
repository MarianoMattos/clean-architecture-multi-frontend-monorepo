using AuditSystem.API.Middleware;
using AuditSystem.Application;
using AuditSystem.Application.Behaviors;
using AuditSystem.Infrastructure;
using FluentValidation;
using MediatR;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("MultiFrontendPolicy", policy =>
    {
        policy.WithOrigins(
                "http://localhost:4200", // Angular Client
                "http://localhost:3000", // React/NextJS Client
                "http://localhost:5173"  // Vue/Vite Client
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(AssemblyReference).Assembly);
    cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(typeof(AssemblyReference).Assembly);

builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddTransient<GlobalExceptionMiddleware>();

var app = builder.Build();

app.UseMiddleware<GlobalExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseCors("MultiFrontendPolicy");

app.MapControllers();

app.Run();