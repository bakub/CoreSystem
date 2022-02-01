using Gateway.Configuration;
using Gateway.Implementations;
using GatewayAPI.Configuration;
using GatewayAPI.Interfaces;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("ocelot.json", false, true);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthCheck(builder.Configuration);
builder.Services.AddOcelot();
builder.Services.AddSingleton<IVersionableService, UserConnectionService>();
builder.Services.AddSingleton<IVersionableService, NotificationConnectionService>();
builder.Services.AddSingleton<IVersionableService, ValidationConnectionService>();
builder.Services.Configure<UrlsConfig>(builder.Configuration.GetSection("Urls"));
builder.Services.AddOptions();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseHealthCheck();

app.MapControllers();
app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/", async context => {    });
});

app.UseOcelot().Wait();

app.Run();