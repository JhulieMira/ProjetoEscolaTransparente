using EscolaTransparente.Domain.Interfaces.Repositories;
using EscolaTransparente.Infraestructure.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EscolaTransparente.Infraestructure.Repository;

namespace EscolaTransparente.IoC.DependencyContainer
{
    public static class DependencyContainer
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration config)
        {
            //Application
            services.AddScoped<Application.Interfaces.Services.IEscolaAppService, Application.Services.EscolaAppService>();

            //Domain
            services.AddScoped<Domain.Interfaces.Services.IEscolaService, Domain.Services.EscolaService>();

            services.AddAutoMapper(typeof(DependencyContainer));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IEscolaRepository, EscolaRepository>();

            services.ConfigureSqlite(config);
        }   
    }
}
