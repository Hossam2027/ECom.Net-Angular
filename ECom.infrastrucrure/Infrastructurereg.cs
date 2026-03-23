using ECom.Core.Interface;
using ECom.infrastrucrure.Data;
using ECom.infrastrucrure.Repositries;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECom.infrastrucrure
{
    public static class InfrastructureRegistration
    {
        public static IServiceCollection AddInfrastructure( this IServiceCollection services , IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("CS"));
            });
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenRepository<>));
           
            services.AddScoped<IUnitofWork, UnitofWork>();
            return services;
        }
    }
}
//services.AddScoped<ICategoryRepository, CategortRepository>();
//services.AddScoped<IProductRepository, ProductRepository>();
//services.AddScoped<IPhotosRepository, IPhotosRepository>();