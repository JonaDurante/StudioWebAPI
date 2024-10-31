using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using StudioBack.Dependency_Injection;
using StudioBack.IdentityExtensionsConfig;
using StudioBack.Middlewares;
using StudioDataAccess;
using StudioDataAccess.Seed;
using System.Reflection;

namespace StudioBack
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseSerilog((HostBuilderCtx, LoggerConf) =>
            {
                LoggerConf
                    .WriteTo.Console()
                    .WriteTo.Debug()
                    .ReadFrom.Configuration(HostBuilderCtx.Configuration);
            });

            var connectionString = builder.Configuration.GetConnectionString("SqlLiteConnection")
                                   ?? throw new InvalidOperationException("Connection string 'StudioContextConnection' not found.");
            builder.Services.AddDbContext<StudioDBContext>(option => option.UseSqlite(connectionString));

            builder.Services.ConfigureAuth(builder);

            builder.Services.RegisterDependencies();

            builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(Program)));

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Autorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization Header"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[]{ }
                    }
                });
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader().AllowCredentials(); ;
                    });
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseCors(x => x.AllowAnyOrigin());

            app.MapControllers();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                DatabaseSeed.SeedUsersAsync(services, builder.Configuration).GetAwaiter().GetResult();
            }

            app.Run();
        }
    }
}
