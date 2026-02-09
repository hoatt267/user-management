using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using UserManagementApp.Application.Common.Behaviors;
using UserManagementApp.Application.Features.Users.Queries.GetUsers;
using UserManagementApp.Domain.Repositories;
using UserManagementApp.Infrastructure.DatabaseContext;
using UserManagementApp.Infrastructure.Repositories.Generic;

namespace UserManagementApp.API
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterExtension(this WebApplicationBuilder builder)
        {
            //add SignalR
            builder.Services.AddSignalR();

            //Connect to database
            builder.Services.AddDbContextPool<UserManagementAppDbContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("UserManagement"), SqlDatabaseOptions));

            RegisterInfrastructure(builder);
            RegisterServices(builder);
        }

        public static void ApplyMigration(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<UserManagementAppDbContext>();

                    // Kiểm tra xem có migration nào chưa chạy không rồi mới Migrate
                    if (context.Database.GetPendingMigrations().Any())
                    {
                        context.Database.Migrate();
                    }
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while migrating the database.");
                    throw;
                }
            }
        }

        private static void SqlDatabaseOptions(SqlServerDbContextOptionsBuilder sqlOptions)
        {
            sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 10,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null);
            sqlOptions.CommandTimeout(300);
        }

        private static void RegisterServices(WebApplicationBuilder builder)
        {
            var assembly = Assembly.GetExecutingAssembly();
            builder.Services.AddAutoMapper(typeof(GetUsersQuery).Assembly);

            // Register FluentValidation
            builder.Services.AddValidatorsFromAssembly(typeof(GetUsersQuery).Assembly);

            // Register MediatR handlers from Application assembly with validation behavior
            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(GetUsersQuery).Assembly);
                cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });
        }

        private static void RegisterInfrastructure(WebApplicationBuilder builder)
        {

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: "MyPolicy",
                    policy =>
                    {
                        policy.WithOrigins(builder.Configuration["AllowedClient:ClientUri"] ?? throw new Exception("AllowedClient:ClientUri is not configured"))
                        .AllowCredentials()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });
            });

            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}