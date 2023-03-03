using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TeamsManagement.Api;
using TeamsManagement.Core;
using TeamsManagement.Items;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersCustom()
                .AddValidators(typeof(ItemIdentifier));

builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(ItemIdentifier)));

builder.Services.AddServices(typeof(CoreIdentifier));

builder.Services.AddSwaggerCustom();

builder.Services.ConfigureDatabase(builder.Configuration.GetConnectionString("PgSql"));

var app = builder.Build();

app.UseErrorHandler();

app.UseRouting();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwaggerCustom();


if (app.Environment.EnvironmentName is "Docker")
{
    Initializer.InitializeDatabase(app);

    Initializer.InitializeTeamsAndPlayers(app);
}

// Configure the HTTP request pipeline.

app.Run();
