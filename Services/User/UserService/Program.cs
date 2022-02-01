using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using UserService.Configuration;
using UserService.Repository;
using UserService.Domain.Configuration;
using UserService.Domain.Context;
using Utils.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks(builder.Configuration);
builder.Services.AddDbContext(builder.Configuration);
builder.Services.AddTransient<IUserRepository, UserRepository>();

builder.Services.AddMassTransitRegistration(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}


app.UseHealthChecks("/hc", new HealthCheckOptions()
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MigrateDatabaseOnStartUp<UserDbContext>();

app.MapControllers();

app.Run();
