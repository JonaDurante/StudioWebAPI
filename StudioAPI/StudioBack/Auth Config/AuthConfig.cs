using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using StudioDataAccess;
using StudioModel.Constant;
using StudioModel.Domain;
using System.Text;

namespace StudioBack.IdentityExtensionsConfig
{
    public static class AuthConfig
    {
        private static readonly string AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
        public static IServiceCollection ConfigureAuth(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services
                .AddIdentity<UserApp, IdentityRole>(option =>
                {
                    option.SignIn.RequireConfirmedAccount = false;
                    option.User.RequireUniqueEmail = true;
                    option.User.AllowedUserNameCharacters = AllowedUserNameCharacters;
                })
                .AddSignInManager<SignInManager<UserApp>>()
                .AddEntityFrameworkStores<StudioDBContext>()
                .AddDefaultTokenProviders();

            services.AddAuthorization(option =>
                option.AddPolicy("AdminPolicy", p =>
                    p.RequireClaim(AuthorizationData.UserClaimName, AuthorizationData.Admin))
                );

            services.AddAuthentication(options =>
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

            return services;
        }
    }
}
