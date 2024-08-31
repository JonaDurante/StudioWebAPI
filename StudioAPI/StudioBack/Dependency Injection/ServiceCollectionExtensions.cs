using StudioDataAccess;
using StudioDataAccess.InterfaceDataAccess;
using StudioService;
using StudioService.LoginService;
using StudioService.LoginService.Imp;

namespace StudioBack.Dependency_Injection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection Register(this IServiceCollection services)
        {
            services.Scan(x =>
                x.FromCallingAssembly()
                    .AddClasses()
                    .AsMatchingInterface()
                    .WithScopedLifetime());
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IRoleService, RoleService>();
            return services;
        }
    }
}
