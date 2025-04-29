using EscolaTransparente.Infraestructure.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EscolaTransparente.Application.Config;
using Microsoft.EntityFrameworkCore;
using EscolaTransparente.Infraestructure.Data;
using EscolaTransparente.Application.Interfaces;
using EscolaTransparente.Domain.Interfaces.Services;
using EscolaTransparente.Domain.Services;

namespace EscolaTransparente.IoC.DependencyContainer
{
    public static class DependencyContainer
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration config)
        {
            //Application
            services.AddScoped<IEscolaAppService, Application.Services.EscolaAppService>();

            //Domain
            services.AddScoped<IEscolaService, EscolaService>();

            //Infrastructure
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddAutoMapper(typeof(AutoMapperConfig));

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });
        }   
    }
}
