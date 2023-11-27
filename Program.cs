using Cinebook.Application;
using Cinebook.Application.Features.MovieSessions;
using Cinebook.Application.Features.Seats;
using Cinebook.Application.Features.Seats.Background;
using Cinebook.Infrastructure.Errors;
using Cinebook.Infrastructure.Persistence;
using Cinebook.Infrastructure.Persistence.Seeder;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddTransient<ExceptionMiddleware>();
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.InitializeMediator();

var envSettings = builder.Services.InitializeEnvSettings(builder.Configuration);

builder.Services.InitializeSwagger();
builder.Services.InitializeDatabase(envSettings);
builder.Services.AddTransient<MovieSessionsFactory>();
builder.Services.AddTransient<SeatsFactory>();
builder.Services.AddScoped<DatabaseSeeder>();
builder.Services.AddHostedService<ReservationReleaseService>();

var app = builder.Build();
var appScope = app.Services.CreateScope();

app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    await appScope.ServiceProvider.GetRequiredService<DatabaseSeeder>().SeedAll();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();