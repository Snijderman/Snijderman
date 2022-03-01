using Snijderman.Common.Mvvm;

namespace Snijderman.Common.Wpf.Mvvm.Services;

public interface IViewModelLocator
{
   T GetPageViewModel<T>() where T : class, IMvvmViewModel;
}
