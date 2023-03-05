using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TeamsManagement.Api;
using TeamsManagement.Core;
using TeamsManagement.Items;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersCustom()
                .AddValidators();

builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(ItemIdentifier)))
                .AddServices(typeof(CoreIdentifier))
                .AddSwaggerCustom()
                .ConfigureDatabase(builder.Configuration.GetConnectionString("PgSql"));

var app = builder.Build();

app.UseErrorHandler()
   .UseRouting()
   .UseSwaggerCustom();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

if (app.Environment.EnvironmentName is "Docker")
{
    Initializer.InitializeDatabase(app);

    Initializer.InitializeTeamsAndPlayers(app);
}

// Configure the HTTP request pipeline.

app.Run();
