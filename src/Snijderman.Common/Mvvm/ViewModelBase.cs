using System.Threading.Tasks;

namespace Snijderman.Common.Mvvm;

public abstract class ViewModelBase : ObservableObject, IMvvmViewModel
{
   public virtual Task LoadAsync() => Task.CompletedTask;

   public virtual Task LoadAsync(object[] args) => Task.CompletedTask;

   // default return true, indicating unload was successful
   public virtual Task<bool> UnloadAsync() => Task.FromResult(true);
}
