using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using StudioBack.Dependency_Injection;
using StudioBack.Middlewares;
using StudioDataAccess;
using StudioModel.Domain;
using System.Reflection;

namespace StudioBack
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Serilog
            builder.Host.UseSerilog((HostBuilderCtx, LoggerConf) =>
            {
                LoggerConf
                    .WriteTo.Console() // Escribe en la consola
                    .WriteTo.Debug()   // Escriba en debug
                    .ReadFrom.Configuration(HostBuilderCtx.Configuration);
            });
            #endregion

            #region Service for entity framework
            var connectionString = builder.Configuration.GetConnectionString("StudioContextConnection")
                                   ?? throw new InvalidOperationException("Connection string 'StudioContextConnection' not found.");
            builder.Services.AddDbContext<StudioDBContext>(option => option.UseSqlite(connectionString));
            #endregion

            #region Identity
            builder.Services
                .AddIdentity<UserApp, IdentityRole>(option =>
                {
                    option.SignIn.RequireConfirmedAccount = false;
                    option.User.RequireUniqueEmail = true;
                    option.User.AllowedUserNameCharacters =
                        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                })
                .AddSignInManager<SignInManager<UserApp>>()
                .AddEntityFrameworkStores<StudioDBContext>()
                .AddDefaultTokenProviders();
            #endregion

            #region Injection Dependency

            builder.Services.Register();

            #endregion

            #region Automapper

            builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(Program)));

            #endregion

            builder.Services.AddAuthentication();

            builder.Services.AddControllers();
            
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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

            app.UseAuthorization();

            app.UseCors(x => x.AllowAnyOrigin());

            app.UseAuthentication();

            app.MapControllers();

            app.Run();
        }
    }
}
