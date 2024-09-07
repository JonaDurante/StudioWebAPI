﻿using StudioService.LoginService;
using StudioService.LoginService.Imp;
using StudioService.Services;

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

            return services;
        }
    }
}
