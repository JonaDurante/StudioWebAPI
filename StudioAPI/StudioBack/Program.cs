using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using StudioDataAccess;
using StudioModel.Domain;
using StudioService;
using StudioService.LoginService;
using StudioService.LoginService.Imp;
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

            builder.Services.AddScoped<IJwtService, JwtService>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IRoleService, RoleService>();

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

            app.UseCors(x => x.AllowAnyOrigin());

            app.UseAuthentication();

            app.MapControllers();

            app.Run();
        }
    }
}
