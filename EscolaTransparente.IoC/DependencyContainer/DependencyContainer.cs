using EscolaTransparente.Infraestructure.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EscolaTransparente.IoC.DependencyContainer
{
    public static class DependencyContainer
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration config)
        {
            services.ConfigureSqlite(config);
        }   
    }
}
