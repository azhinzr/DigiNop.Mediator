using FluentScheduler;
using Mediator.ACL.Digikala.services;
using Mediator.ACL.NopCommerce.services;
using Mediator.Jobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Mediator
{
    public class JobRegistery : Registry
    {
        public JobRegistery(IConfiguration configuration, IServiceProvider serviceProvider)
        {
            var nopService = serviceProvider.GetService<INopService>();
            var digiService = serviceProvider.GetService<IDigikalaService>(); 
            Schedule(() => new  ProductSynchronizer(nopService, digiService)).ToRunNow().AndEvery(int.Parse(configuration["Scheduler:DigikalaProductSynchronizerEveryHours"])).Hours();  
         } 
     } 
}
 