using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using TeamsManagement.Api.Infrastructure.Attributes;
using TeamsManagement.Api.Infrastructure.Middlewares;
using TeamsManagement.Api.Infrastructure.Swagger;
using TeamsManagement.Core.Services;
using TeamsManagement.Data;
using TeamsManagement.Items.Exceptions;

namespace TeamsManagement.Api
{
    public static class Extensions
    {
        public static IMvcBuilder AddControllersCustom(this IServiceCollection services)
        {
            return services.AddControllers(opt =>
            {
                opt.Filters.Add<ModelValidatorAttribute>();
            });
        }

        public static IApplicationBuilder UseErrorHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlerMiddleware>();
        }

        public static IServiceCollection AddServices(this IServiceCollection services, Type implementationType)
        {
            services.AddTransient<IServiceBase, ServiceBase>();

            var repositories = implementationType.Assembly.GetTypes().Where(t => t.GetInterfaces().Any(i => i.IsAssignableFrom(typeof(IServiceBase))) && !t.IsInterface);

            foreach (var repository in repositories)
            {
                var repositoryType = repository.GetInterfaces().Where(x => x.Name.Contains(repository.Name)).FirstOrDefault() ??
                                     throw new BusinessException($"Base class did not found for type: {repository.FullName}", 500);

                services.AddTransient(repositoryType, repository);
            }

            return services;
        }

        public static void AddValidators(this IMvcBuilder builder, Type validatorsAssemblyType)
        {
            builder.Services.AddValidatorsFromAssembly(validatorsAssemblyType.Assembly);

            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (context) =>
                {
                    var firstMessage = context.ModelState.Values.Where(v => v.Errors.Count > 0)
                                                                .SelectMany(v => v.Errors)
                                                                .Select(v => new
                                                                {
                                                                    Message = (!string.IsNullOrEmpty(v.ErrorMessage) || v.Exception is null) ? v.ErrorMessage : v.Exception.Message
                                                                })
                                                                .FirstOrDefault();

                    return new BadRequestObjectResult(new ErrorModel()
                    {
                        Message = firstMessage?.Message
                    });
                };
            });
        }

        public static IServiceCollection AddSwaggerCustom(this IServiceCollection services)
        {
            SwaggerOptions options;

            using (var serviceProvider = services.BuildServiceProvider())
            {
                var configuration = serviceProvider.GetService<IConfiguration>();

                var section = configuration?.GetSection("swagger");
                options = new SwaggerOptions();

                if (section != null)
                {
                    services.Configure<SwaggerOptions>(section);
                    section.Bind(options);
                }
            }

            return services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(options?.Version, new OpenApiInfo { Title = options?.Title, Version = options?.Version });

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });
        }

        public static IApplicationBuilder UseSwaggerCustom(this IApplicationBuilder builder)
        {
            var options = new SwaggerOptions();

            builder.ApplicationServices
                   .GetService<IConfiguration>()?
                   .GetSection("swagger").Bind(options);

            builder.UseSwagger();

            builder.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", options.Title);
            });

            return builder;
        }

        public static void ConfigureDatabase(this IServiceCollection services, string? connectionString)
        {
            services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connectionString, x =>
            {
                x.MigrationsAssembly(typeof(DataIdentifier).Namespace);
                x.MigrationsHistoryTable("_Migrations", "TeamsManagement");
            }));

            services.AddScoped(typeof(DbContext), typeof(ApplicationContext));
        }
    }
}
