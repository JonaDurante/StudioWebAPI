
using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using StudioDataAccess;

namespace StudioBack
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
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
            builder.Services.AddDbContext<StudioDBContext>(option => option.UseSqlServer(connectionString));
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

            //builder.Services.AddScoped<>();

            #endregion

            #region Automapper

            builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(Program)));

            #endregion


            builder.Services.AddAuthentication();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseAuthentication();

            app.MapControllers();

            app.Run();
        }
    }
}
