using Hangfire;
using NotificationService.Configuration;
using NotificationService.Domain.Context;
using NotificationService.Domain.Configuration;
using MassTransit;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;
using Utils.Extensions;

var builder = WebApplication.CreateBuilder(args);


builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddMassTransit(builder.Configuration)
    .AddHangfire()
    .AddDbContext(builder.Configuration)
    .AddServices()
    .AddHealthChecks(builder.Configuration);
    

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app
    .UseHttpsRedirection()
    .UseAuthorization()
    .UseHangfireDashboard()
    .UseHangfireJobs()
    .MigrateDatabaseOnStartUp<NotificationsDbContext>()
    .UseHealthChecks("/hc", new HealthCheckOptions()
    {
        Predicate = _ => true,
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

app.MapControllers();

app.Run();
