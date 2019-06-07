using GeekBurger.Production.Application.Interfaces;
using GeekBurger.Production.Application.Service;
using GeekBurger.Production.Infra.Repository;
using GeekBurger.Production.Interface;
using GeekBurger.Production.Models;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace GeekBurger.Production.Infra.Ioc
{
    public static class Bootstrapper
    {
        private static IServiceProvider ServiceProvider { get; set; }

        private static IServiceCollection Services { get; set; }

        public static T GetService<T>()
        {
            Services = Services ?? RegisterServices();
            ServiceProvider = ServiceProvider ?? Services.BuildServiceProvider();
            return ServiceProvider.GetService<T>();
        }

        public static IServiceCollection RegisterServices()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            IConfiguration configuration = builder.Build();

            return RegisterServices(new ServiceCollection(), configuration);
        }

        public static IServiceCollection RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            Services = services;
            
            services.Configure<NoSql>(configuration.GetSection("NoSql"));

            //Inject here
            services.AddSingleton<IConfiguration>(configuration);

            services.AddSingleton(a => new DocumentClient(new Uri(configuration["Nosql:Uri"]), configuration["Nosql:Key"]));

            services.AddSingleton<IProductionRepository, ProductionRepository>();

            services.AddSingleton<ILogService, LogService>();
            services.AddSingleton<IOrderService, OrderService>();

            return Services;
        }
    }
}
