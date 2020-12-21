using System;
using System.Threading.Tasks;

namespace Snijderman.Common.Mvvm
{
   public interface INavigationService
   {
      Task NavigateToAsync<VM>(Func<VM, IMvvmControl<VM>, Task> handleNavigation) where VM : IMvvmViewModel;
   }
}
