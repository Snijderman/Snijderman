using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Snijderman.Common.Blazor.Components;
using Snijderman.Samples.Blazor.Mvvm.ViewModels;

namespace Snijderman.Samples.Blazor.Mvvm.Components
{
   public partial class OrderDetails : MvvmViewModelComponentBase<OrderViewModel>
   {
      [Parameter] public CustomerViewModel Customer { get; set; }

      private long _selectedOrderId = -1;
      protected long SelectedOrderId => this._selectedOrderId;

      public async Task SetOrderViewModelAsync(long orderId)
      {
         if (this.Customer == default)
         {
            return;
         }
         this._selectedOrderId = orderId;
         this.ViewModel = this.Customer.Orders.FirstOrDefault(x => x.OrderId == orderId);
         await Task.Delay(TimeSpan.FromSeconds(1.5)).ConfigureAwait(false);
         await this.ViewModel.LoadAsync().ConfigureAwait(false);
      }
   }
}
