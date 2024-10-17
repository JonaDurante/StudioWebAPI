using StudioDataAccess.Repositories;
using StudioDataAccess.Repositories.Imp;
using StudioDataAccess.Uow;
using StudioDataAccess.Uow.Imp;
using StudioService.LoginService;
using StudioService.LoginService.Imp;
using StudioService.Services;
using StudioService.Services.Imp;

namespace StudioBack.Dependency_Injection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterDependencies(this IServiceCollection services)
        {
            services.Scan(x =>
                x.FromCallingAssembly()
                    .AddClasses()
                    .AsMatchingInterface()
                    .WithScopedLifetime());

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserProfileService, UserProfileService>();
            services.AddTransient<IEmailService, EmailService>();

            services.AddScoped<IUserProfileRepository, UserProfileRepository>();
            services.AddScoped<IEmailSettingsRepository, EmailSettingsRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();


            return services;
        }
    }
}
