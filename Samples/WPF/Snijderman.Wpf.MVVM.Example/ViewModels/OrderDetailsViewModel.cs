using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Snijderman.Common.Mvvm.Services;
using Snijderman.Common.Wpf.Commands;
using Snijderman.Common.Wpf.Mvvm.Services;
using Snijderman.Samples.Common.Model;
using Snijderman.Samples.Common.Services;

namespace Snijderman.Wpf.MVVM.Example.ViewModels;

public class OrderDetailsViewModel : WpfMvvmViewModelBase
{
   private readonly IOrderDetailsService _orderDetailsService;
   private readonly IMessageService _messageService;
   private readonly IDialogService _dialogService;

   public OrderDetailsViewModel(IOrderDetailsService orderDetailsService, IMessageService messageService, IDialogService dialogService)
   {
      this._orderDetailsService = orderDetailsService;
      this._messageService = messageService;
      this._dialogService = dialogService;
   }

   public override async Task LoadAsync(object[] args)
   {
      if (args == default || args.Length != 1 || args[0] is not long orderId)
      {
         throw new ArgumentException("No valid customer ID provided");
      }
      this.OrderDetails = new ObservableCollection<OrderDetail>(await this._orderDetailsService.GetOrderDetails(orderId).ConfigureAwait(false));
   }

   private ObservableCollection<OrderDetail> _orderDetails;
   public ObservableCollection<OrderDetail> OrderDetails
   {
      get => this._orderDetails;
      set => this.Set(ref this._orderDetails, value);
   }

   private OrderDetail _selected;
   public OrderDetail Selected
   {
      get => this._selected;
      set => this.Set(ref this._selected, value);
   }

   public ICommand ShowFullOrderDetailsCommand => new AsyncCommand<OrderDetail>(this.ShowOrderDetailsFull, orderDetail => orderDetail != default);

   private Task ShowOrderDetailsFull(OrderDetail orderDetail)
   {
      this._dialogService.Show("Order details", $"Order detail '{orderDetail.ShortDescription}' selected");
      return Task.CompletedTask;
   }
}
