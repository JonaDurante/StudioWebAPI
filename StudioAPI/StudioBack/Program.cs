using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using StudioBack.Dependency_Injection;
using StudioBack.Middlewares;
using StudioDataAccess;
using StudioDataAccess.Seed;
using StudioModel.Constant;
using StudioModel.Domain;
using System.Reflection;
using System.Text;

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

            builder.Services.Register();

            builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(Program)));

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = true;
                o.SaveToken = true;

                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration[key: "JsonWebTokenKeys:IsUserSigninKey"]!)),
                    ValidateIssuer = false,
                    ValidIssuer = builder.Configuration[key: "JsonWebTokenKeys:ValidIsUser"],
                    ValidateAudience = false,
                    ValidAudience = builder.Configuration[key: "JsonWebTokenKeys:ValidAudience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            builder.Services.AddAuthorization(option =>
                option.AddPolicy("AdminPolicy", p =>
                    p.RequireClaim(AuthorizationData.UserClaimName, AuthorizationData.Admin))
                );

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
                DatabaseSeed.SeedUsersAsync(services).GetAwaiter().GetResult();
            }

            app.Run();
        }
    }
}
