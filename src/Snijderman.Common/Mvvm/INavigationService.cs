using System;
using System.Threading.Tasks;

namespace Snijderman.Common.Mvvm;

public interface INavigationService
{
   Task NavigateToAsync<TVm>(Func<TVm, IMvvmControl<TVm>, Task> handleNavigation) where TVm : IMvvmViewModel;

   Task NavigateToAsync(Type viewModelType, Func<IMvvmViewModel, IMvvmControl<IMvvmViewModel>, Task> handleNavigation);
}
