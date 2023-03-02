using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TeamsManagement;
using TeamsManagement.Core;
using TeamsManagement.Items;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllersCustom()
                .AddValidators(typeof(ItemIdentifier));

builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(ItemIdentifier)));

builder.Services.AddServices(typeof(CoreIdentifier));

builder.Services.AddSwaggerCustom();

builder.Services.ConfigureDatabase(builder.Configuration.GetConnectionString("PgSql"));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseErrorHandler();

app.UseRouting();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwaggerCustom();


if (app.Environment.EnvironmentName == "Docker")
{
    Initializer.InitializeDatabase(app);

    Initializer.InitializeTeamsAndPlayers(app);
}

// Configure the HTTP request pipeline.

app.Run();
