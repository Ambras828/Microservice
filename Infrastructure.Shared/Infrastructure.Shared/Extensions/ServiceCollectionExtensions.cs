using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Shared.Database;
using Infrastructure.Shared.Authentication;
using Microsoft.Extensions.Configuration;
using Infrastructure.Shared.Middleware;
using Microsoft.AspNetCore.Builder;


namespace Infrastructure.Shared.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSharedInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();

            JWTAuthenticationScheme.AddJWTAuthenticatioSchema(services, config);
            return services;
        }

       
    }
}