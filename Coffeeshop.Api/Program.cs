using Coffeeshop.Api.Validations;
using Coffeeshop.Infrastructure;
using FluentValidation;
using MediatR;
using Serilog;
using Serilog.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddMediatR(typeof(Coffeeshop.Domain.Bootstrapper).Assembly);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddValidatorsFromAssemblyContaining<CoffeeCreateValidator>();

builder.Logging.AddConsole();

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.ControlledBy(new Serilog.Core.LoggingLevelSwitch())
    .WriteTo.File("./logs/coffee.api.logs")
    .CreateLogger();

builder.Logging.AddProvider(new SerilogLoggerProvider());

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
