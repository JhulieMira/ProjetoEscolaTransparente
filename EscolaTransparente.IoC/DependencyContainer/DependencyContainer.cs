using EscolaTransparente.Domain.Interfaces.Repositories;
using EscolaTransparente.Infraestructure.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EscolaTransparente.Application.Config;
using Microsoft.EntityFrameworkCore;
using EscolaTransparente.Infraestructure.Data;
using EscolaTransparente.Application.Interfaces.Services;

namespace EscolaTransparente.IoC.DependencyContainer
{
    public static class DependencyContainer
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration config)
        {
            //Application
            services.AddScoped<IEscolaAppService, Application.Services.EscolaAppService>();

            services.AddAutoMapper(typeof(AutoMapperConfig));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });
        }   
    }
}
