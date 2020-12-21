using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Snijderman.Common.Mvvm
{
   public abstract class NavigationServiceBase : INavigationService
   {
      protected readonly IMvvmControlService _mvvmControlService;
      protected readonly IServiceProvider _services;

      public NavigationServiceBase(IMvvmControlService mvvmControlService, IServiceProvider services)
      {
         this._mvvmControlService = mvvmControlService;
         this._services = services;
      }

      public async Task NavigateToAsync<VM>(Func<VM, IMvvmControl<VM>, Task> handleNavigation) where VM : IMvvmViewModel
      {
         var controlToShow = this._mvvmControlService.GetControl<VM>();
         if (controlToShow == default)
         {
            throw new MvvmException($"Unable to get view for viewmodel {typeof(VM).FullName}");
         }

         var vm = controlToShow.GetViewModel();
         if (vm is not VM viewModel)
         {
            viewModel = this._services.GetRequiredService<VM>();
         }

         await handleNavigation(vm ?? viewModel, controlToShow);
      }
   }
}
