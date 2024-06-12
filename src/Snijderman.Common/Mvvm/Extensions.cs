using Microsoft.Extensions.DependencyInjection;

namespace Snijderman.Common.Mvvm;

public static class Extensions
{
   public static void AddViewModelAndControl<TVm, TV>(this IServiceCollection services) where TVm : class, IMvvmViewModel
                                                                                        where TV : class, IMvvmControl<TVm>
   {
      services.AddTransient<TVm>();
      services.AddTransient<TV>();
      MvvmControlService.AddViewModelWithControl<TVm, TV>();
   }
}
