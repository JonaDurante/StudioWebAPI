using StudioDataAccess;
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
           .WithScopedLifetime()
           );

            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IJwtService, JwtService>();

            return services;
        }
    }
}
