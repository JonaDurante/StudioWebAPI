namespace StudioBack.Dependency_Injection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection Register(this IServiceCollection services)
        {
            services.Scan(x =>
            x.FromCallingAssembly()
             .AddClasses()
             .AsMatchingInterface());

            return services;
        }
    }
}
