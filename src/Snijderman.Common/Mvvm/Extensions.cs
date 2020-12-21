using Microsoft.Extensions.DependencyInjection;

namespace Snijderman.Common.Mvvm
{
   public static class Extensions
   {
      public static void AddViewModelAndControl<VM, V>(this IServiceCollection services) where VM : class, IMvvmViewModel
                                                                                         where V : class, IMvvmControl<VM>
      {
         services.AddTransient<VM>();
         services.AddTransient<V>();
         MvvmControlService.AddViewModelWithControl<VM, V>();
      }
   }
}
