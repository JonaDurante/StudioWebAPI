using StudioDataAccess.Repositories;
using StudioDataAccess.Uow;
using StudioDataAccess.Uow.Imp;

namespace StudioBack.Dependency_Injection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterDependencies(this IServiceCollection services)
        {
            services.Scan(scan => scan
                   .FromCallingAssembly()
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
