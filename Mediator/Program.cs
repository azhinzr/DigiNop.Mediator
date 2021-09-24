using ApiClient;
using ApiClient.Digikala;
using ApiClient.NopCommerce;
using FluentScheduler;
using Mediator.ACL.Digikala;
using Mediator.ACL.Digikala.services;
using Mediator.ACL.NopCommerce;
using Mediator.ACL.NopCommerce.services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Mediator
{
    class Program
    {
        public static IConfiguration Configuration;

        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
     .AddJsonFile("Appsettings.json", optional: true, reloadOnChange: true);
            Configuration = builder.Build();
            var serviceProvider = new ServiceCollection()
            .AddSingleton(Configuration)
            .AddTransient<INopAliClient, NopApiClient> ()
            .AddTransient<IDigikalaApiClient, DigikalaApiClient>()
             .AddTransient<INopService, NopService>()
              .AddTransient<IDigikalaService, DigikalaService>()

            .BuildServiceProvider();

            JobManager.Initialize(new JobRegistery(Configuration, serviceProvider));
            Console.ReadLine();
        }
    }
}
