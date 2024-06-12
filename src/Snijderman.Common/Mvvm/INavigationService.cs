using System;
using System.Threading.Tasks;

namespace Snijderman.Common.Mvvm;

public interface INavigationService
{
   Task NavigateToAsync<TVm>(Func<TVm, IMvvmControl<TVm>, Task> handleNavigation) where TVm : IMvvmViewModel;
}
