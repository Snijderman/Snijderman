using System.Threading.Tasks;

namespace Snijderman.Common.Mvvm
{
   public abstract class ViewModelBase : ObservableObject, IMvvmViewModel
   {
      public virtual Task LoadAsync()
      {
         return Task.CompletedTask;
      }

      public virtual Task LoadAsync(object[] args)
      {
         return Task.CompletedTask;
      }

      public virtual Task<bool> UnloadAsync()
      {
         // default return true, indicating unload was successful
         return Task.FromResult(true);
      }
   }
}
