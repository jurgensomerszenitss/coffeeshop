using Coffeeshop.Api.Controllers;
using Coffeeshop.Api.Validations;
using Coffeeshop.Infrastructure;
using FluentValidation;
using Mapster;
using MediatR;
using Serilog;
using System.Diagnostics.CodeAnalysis;

[assembly: ExcludeFromCodeCoverage]
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

TypeAdapterConfig.GlobalSettings.Scan(typeof(CoffeeController).Assembly, typeof(Coffeeshop.Domain.Bootstrapper).Assembly);

builder.Services.AddControllers();
builder.Services.AddMediatR(typeof(Coffeeshop.Domain.Bootstrapper).Assembly);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddValidatorsFromAssemblyContaining<CoffeeCreateValidator>();
builder.Services.AddApplicationInsightsTelemetry(); // configure logging to AppInsights
builder.Services.AddSwaggerGen();

builder.Logging.AddConsole();

builder.Services.AddLogging(x =>
{
    x.ClearProviders();
    x.AddSerilog(dispose: true);
});

builder.Host.UseSerilog((ctx, services, lc) =>
    lc.ReadFrom.Configuration(ctx.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext()
    .WriteTo.Console());

Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateBootstrapLogger();
Log.ForContext<Program>().Information("Starting up Coffeeshop.Api");



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(cfg =>
{
    cfg.AllowAnyOrigin();
    cfg.WithMethods("GET", "POST", "PUT", "DELETE", "OPTIONS", "PATCH");
    cfg.WithHeaders("Content-Type");
});

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseHttpLogging(); // add logging for each http request
app.UseSerilogRequestLogging();
app.Run();
