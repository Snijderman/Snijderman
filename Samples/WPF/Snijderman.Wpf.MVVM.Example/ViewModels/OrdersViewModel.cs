using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Controls;
using Snijderman.Common.Mvvm;
using Snijderman.Common.Mvvm.Services;
using Snijderman.Common.Wpf.Extensions;
using Snijderman.Samples.Common.Model;
using Snijderman.Samples.Common.Services;

namespace Snijderman.Wpf.MVVM.Example.ViewModels;

public class OrdersViewModel : ItemSelectedViewModelBase<Order>, IWpfMvvmViewModel
{
   private readonly IOrderService _orderService;
   private readonly IMessageService _messageService;
   private readonly INavigationService _navigationService;

   public OrdersViewModel(IOrderService orderService, IMessageService messageService, INavigationService navigationService)
   {
      this._orderService = orderService;
      this._messageService = messageService;
      this._navigationService = navigationService;
      this.SelectedItemChanged += this.LoadOrderDetails;
   }

   public override async Task LoadAsync(object[] args)
   {
      if (args == default || args.Length != 1 || args[0] is not string customerId)
      {
         throw new ArgumentException("No valid customer ID provided");
      }

      this.Orders = new ObservableCollection<Order>(await this._orderService.GetOrdersAsync(customerId).ConfigureAwait(false));
   }

   private ObservableCollection<Order> _orders;
   public ObservableCollection<Order> Orders
   {
      get => this._orders;
      set => this.Set(ref this._orders, value);
   }

   public ContentControl VmContentControl { get; set; } = new ContentControl();

   private async Task LoadOrderDetails(Order order)
   {
      if (order == default)
      {
         this._messageService.Send(this, MessageConstants.StatusMessage, $"No order selected");
         return;
      }

      this._messageService.Send(this, MessageConstants.StatusMessage, $"Order '{order.OrderID}' selected");
      await this._navigationService.NavigateToAsync<OrderDetailsViewModel>(async (viewModel, controlToShow) =>
      {
         this.VmContentControl.Content = controlToShow;
         await viewModel.LoadAsync(new object[] { order.OrderID }).ConfigureAwait(false);
      }).ConfigureAwait(false);
   }
}
