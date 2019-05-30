﻿using GeekBurger.Production.Application.Interfaces;
using GeekBurger.Production.Application.Service;
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

            //Inject here
            services.AddSingleton(a => new DocumentClient(new Uri("https://geekburger-production.documents.azure.com:443/"), "JlpD6frg19nvq0TMAIrQcFi9zeZEMW8NEUhYMrzbBVLMmvehraJRLQ6tfGM75RYgnSJu525hLh5yklnudzi8jA=="));

            services.AddSingleton<ILogService, LogService>();
            services.AddSingleton<IOrderService, OrderService>();

            return Services;
        }
    }
}
