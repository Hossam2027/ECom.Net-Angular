using ECom.Core.Interface;
using ECom.Core.Services;
using ECom.infrastrucrure.Data;
using ECom.infrastrucrure.Repositries;
using ECom.infrastrucrure.Repositries.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
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
            //apply redis connection
            //services.AddSingleton<IConnectionMultiplexer>(sp =>
            //{
            //    var configurationOptions = ConfigurationOptions.Parse(configuration.GetConnectionString("Redis"), true);
            //    return ConnectionMultiplexer.Connect(configurationOptions);
            //});
            services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                var options = ConfigurationOptions.Parse(
                    configuration.GetConnectionString("Redis")
                );

                options.AbortOnConnectFail = false;
                options.Ssl = true;

                return ConnectionMultiplexer.Connect(options);
            });
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IPaymentServices, PaymentServices>();
            services.AddScoped<IUnitofWork, UnitofWork>();
            services.AddScoped<IOrderServices, OrderServices>();

            return services;
        }
    }
}
//services.AddScoped<ICategoryRepository, CategortRepository>();
//services.AddScoped<IProductRepository, ProductRepository>();
//services.AddScoped<IPhotosRepository, IPhotosRepository>();