using System.Threading.Tasks;

namespace Snijderman.Common.Mvvm;

public abstract class ItemSelectedViewModelBase<T> : ViewModelBase
{
   public delegate Task SelectedItemChangedEventHandler(T selectedItem);
   public event SelectedItemChangedEventHandler SelectedItemChanged;

   private T _selected;
   public T Selected
   {
      get => this._selected;
      set
      {
         if (this.Set(ref this._selected, value))
         {
            this.OnSelectedItemChanged(value);
         }
      }
   }

   protected virtual void OnSelectedItemChanged(T selectedItem)
   {
      this.SelectedItemChanged?.Invoke(selectedItem);
   }
}
