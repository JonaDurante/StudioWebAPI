using StudioDataAccess.Repositories;
using StudioDataAccess.Repositories.Imp;
using StudioDataAccess.Uow;
using StudioDataAccess.Uow.Imp;
using StudioService.LoginService;
using StudioService.Services;

namespace StudioBack.Dependency_Injection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterDependencies(this IServiceCollection services)
        {
            services.Scan(scan => scan
                   .FromAssemblyOf<IAccountService>()
                   .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Service")))
                   .AsImplementedInterfaces()
                   .WithScopedLifetime());

            services.Scan(scan => scan
                    .FromAssemblyOf<IUserProfileRepository>()
                    .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Repository")))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
