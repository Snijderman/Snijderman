using Microsoft.Extensions.DependencyInjection;
using Snijderman.Samples.Common.Services;

namespace Snijderman.Samples.Common.ExtensionMethods
{
   public static class ServicesCollectionExtensions
   {
      public static IServiceCollection RegisterSampleCommonServices(this IServiceCollection services)
      {
         services.AddTransient<ISampleDataService, SampleDataService>();
         services.AddTransient<ICustomerService, CustomerService>();
         services.AddTransient<IOrderService, OrderService>();
         services.AddTransient<IOrderDetailsService, OrderDetailsService>();

         return services;
      }
   }
}
