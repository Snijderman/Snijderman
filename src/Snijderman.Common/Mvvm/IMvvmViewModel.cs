using System.Threading.Tasks;

namespace Snijderman.Common.Mvvm
{
   public interface IMvvmViewModel
   {
      Task LoadAsync();

      Task LoadAsync(object[] args);

      Task<bool> UnloadAsync();
   }
}
