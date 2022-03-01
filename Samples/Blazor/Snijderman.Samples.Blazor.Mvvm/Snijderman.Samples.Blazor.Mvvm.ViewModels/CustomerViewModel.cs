using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Snijderman.Common.Mvvm;
using Snijderman.Samples.Common.Model;
using Snijderman.Samples.Common.Services;

namespace Snijderman.Samples.Blazor.Mvvm.ViewModels;

public class CustomerViewModel : ItemSelectedViewModelBase<OrderViewModel>
{
   private readonly IOrderService _orderService;
   private readonly IServiceProvider _serviceProvider;

   public delegate Task CustomerSelectedEventHandler();
   public event CustomerSelectedEventHandler CustomerSelected;

   public delegate Task OrdersLoadedEventHandler();
   public event OrdersLoadedEventHandler OrdersLoaded;

   public CustomerViewModel(IOrderService orderService, IServiceProvider serviceProvider)
   {
      this._orderService = orderService;
      this._serviceProvider = serviceProvider;
      this.CustomerSelected += this.OnCustomerSelected;
   }

   private string _companyId;
   public string CompanyId
   {
      get => this._companyId;
      set => this.Set(ref this._companyId, value);
   }

   private string _companyName;
   public string CompanyName
   {
      get => this._companyName;
      set => this.Set(ref this._companyName, value);
   }

   private ObservableCollection<OrderViewModel> _orders = new();
   public ObservableCollection<OrderViewModel> Orders
   {
      get => this._orders;
      set => this.Set(ref this._orders, value);
   }

   public bool IsEnabled => this.Orders?.Count > 0;

   public bool IsVisible { get; set; } = true;

   private Guid _uniqueId = Guid.NewGuid();
   public Guid UniqueId
   {
      get => this._uniqueId;
      set => this.Set(ref this._uniqueId, value);
   }

   private bool _isSelected;
   public bool IsSelected
   {
      get => this._isSelected;
      set
      {
         if (this.Set(ref this._isSelected, value))
         {
            if (this.IsSelected)
            {
               this.CustomerSelected.Invoke();
            }
         }
      }
   }

   private bool _isLoaded;
   public bool IsLoaded
   {
      get => this._isLoaded;
      set
      {
         if (this.Set(ref this._isLoaded, value))
         {
            if (this.IsLoaded)
            {
               this.OrdersLoaded?.Invoke();
            }
         }
      }
   }

   private async Task OnCustomerSelected()
   {
      if (!this.IsLoaded)
      {
         await Task.Delay(TimeSpan.FromSeconds(1.5)).ConfigureAwait(false);
         await this.LoadAsync().ConfigureAwait(false);
         this.IsLoaded = true;
      }
   }

   public override async Task LoadAsync() => this.Orders = this.GetCustomerOrders(await this._orderService.GetOrdersAsync(this.CompanyId).ConfigureAwait(false));

   private ObservableCollection<OrderViewModel> GetCustomerOrders(IEnumerable<Order> orders)
   {
      return new ObservableCollection<OrderViewModel>(orders.Select(x => this._serviceProvider.CreateAndFillViewModelProperties<OrderViewModel, Order>(x, Helper.SetOrderViewModelProperties)));
   }
}
