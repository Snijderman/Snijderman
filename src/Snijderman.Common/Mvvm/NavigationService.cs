using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Snijderman.Common.Helpers;

namespace Snijderman.Common.Mvvm;

public class NavigationService : INavigationService
{
   private readonly IMvvmControlService _mvvmControlService;
   private readonly IServiceProvider _services;

   public NavigationService(IMvvmControlService mvvmControlService, IServiceProvider services)
   {
      this._mvvmControlService = mvvmControlService;
      this._services = services;
   }

   public virtual async Task NavigateToAsync<TVm>(Func<TVm, IMvvmControl<TVm>, Task> handleNavigation) where TVm : IMvvmViewModel
   {
      var controlToShow = this._mvvmControlService.GetControl<TVm>();
      if (controlToShow == default)
      {
         throw new MvvmException($"Unable to get view for viewmodel {typeof(TVm).FullName}");
      }

      var vm = controlToShow.GetViewModel();
      if (vm is not TVm viewModel)
      {
         viewModel = this._services.GetRequiredService<TVm>();
      }

      await handleNavigation(vm ?? viewModel, controlToShow).ConfigureAwait(false);
   }

   public async Task NavigateToAsync(Type viewModelType, Func<IMvvmViewModel, IMvvmControl<IMvvmViewModel>, Task> handleNavigation)
   {
      ArgumentNullException.ThrowIfNull(viewModelType);

      // check if viewModelType is a subclass of IMvvmViewModel
      if (!typeof(IMvvmViewModel).IsAssignableFrom(viewModelType))
      {
         throw new ArgumentException($"Type {viewModelType.FullName} does not implement {nameof(IMvvmViewModel)}");
      }

      await Reflection.MakeGenericMethodAndInvokeAsync(this, nameof(this.NavigateToAsync), [viewModelType], [handleNavigation]).ConfigureAwait(false);
   }
}
