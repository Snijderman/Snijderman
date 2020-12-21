using Microsoft.Extensions.DependencyInjection;
using Snijderman.Common.Mvvm;
using Snijderman.Common.Wpf.Mvvm.Services;

namespace Snijderman.Common.Wpf.Mvvm
{
   public static class Extension
   {
      public static IServiceCollection RegisterAllDefaultMvvmSerices(this IServiceCollection services)
      {
         // App Host
         services.AddHostedService<ApplicationHostService>();

         // Services
         services.AddSingleton<INavigationService, NavigationServiceBase>();
         services.AddSingleton<IMvvmControlService, MvvmControlService>();
         services.AddSingleton<IMessageService, MessageService>();
         services.AddTransient<IDialogService, DialogService>();
         services.AddSingleton<IViewModelLocator, ViewModelLocator>();

         return services;
      }
   }
}
