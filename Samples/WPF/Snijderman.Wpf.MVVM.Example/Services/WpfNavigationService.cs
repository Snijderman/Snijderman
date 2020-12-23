using System;
using System.Threading.Tasks;
using Snijderman.Common.Mvvm;
using Snijderman.Wpf.MVVM.Example.ViewModels;

namespace Snijderman.Wpf.MVVM.Example.Services
{
   public class WpfNavigationService : NavigationService
   {
      public WpfNavigationService(IMvvmControlService mvvmControlService, IServiceProvider services) : base(mvvmControlService, services)
      {

      }

      public new async Task NavigateToAsync<VM>(Func<VM, IMvvmControl<VM>, Task> handleNavigation) where VM : IWpfMvvmViewModel => await base.NavigateToAsync(handleNavigation);
   }
}
