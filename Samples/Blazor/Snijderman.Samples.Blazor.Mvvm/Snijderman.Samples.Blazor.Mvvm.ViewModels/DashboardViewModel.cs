using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Snijderman.Common.Mvvm;
using Snijderman.Common.Mvvm.Services;
using Snijderman.Samples.Common.Model;
using Snijderman.Samples.Common.Services;

namespace Snijderman.Samples.Blazor.Mvvm.ViewModels;

public class DashboardViewModel : ItemSelectedViewModelBase<CustomerViewModel>
{
   private readonly ICustomerService _customerService;
   private readonly IMessageService _messageService;
   private readonly IServiceProvider _serviceProvider;

   public DashboardViewModel(ICustomerService customerService, IMessageService messageService, IServiceProvider serviceProvider)
   {
      this._customerService = customerService;
      this._messageService = messageService;
      this._serviceProvider = serviceProvider;
      this.SelectedItemChanged += this.LoadCustomerOrders;
   }

   public override async Task LoadAsync() => this._customers = new ObservableCollection<CustomerViewModel>(this.GetCustomers(await this._customerService.GetCustomersAsync().ConfigureAwait(false)));
   private IEnumerable<CustomerViewModel> GetCustomers(IEnumerable<Customer> customers)
   {
      foreach (var customer in customers)
      {
         yield return this._serviceProvider.CreateAndFillViewModelProperties<CustomerViewModel, Customer>(customer, Helper.SetCustomerViewModelProperties);
         //yield return this.CreateCustomerViewModel(customer.CompanyID, customer.CompanyName);
      }
   }

   //private CustomerViewModel CreateCustomerViewModel(string companyId, string companyName)
   //{
   //   this._serviceProvider.CreateAndFillViewModelProperties<CustomerViewModel, Customer>(default, Helper.SetCustomerViewModelProperties);
   //   //var customer = this._serviceProvider.GetRequiredService<CustomerViewModel>();
   //   //customer.CompanyId = companyId;
   //   //customer.CompanyName = companyName;
   //   //return customer;
   //}

   private ObservableCollection<CustomerViewModel> _customers;
   public ObservableCollection<CustomerViewModel> Customers
   {
      get => this._customers;
      set => this.Set(ref this._customers, value);
   }

   protected override void OnSelectedItemChanged(CustomerViewModel selectedItem)
   {
      this.SelectedItem = selectedItem?.CompanyId;
      foreach (var customer in this.Customers)
      {
         customer.IsSelected = customer.CompanyId == selectedItem?.CompanyId;
      }
      base.OnSelectedItemChanged(selectedItem);
   }

   private string _selectedItem;
   public string SelectedItem
   {
      get => this._selectedItem;
      set => this.Set(ref this._selectedItem, value);
   }

   public void SetSelectedTab(string id) => this.Selected = this.Customers.FirstOrDefault(x => string.Equals(x.CompanyId, id, StringComparison.OrdinalIgnoreCase));

   private async Task LoadCustomerOrders(CustomerViewModel customer)
   {
      if (customer == default)
      {
         this._messageService.Send(this, MessageConstants.StatusMessage, "No customer selected");
         return;
      }

      this._messageService.Send(this, MessageConstants.StatusMessage, $"Customer '{customer.CompanyId}' selected");
      await Task.CompletedTask.ConfigureAwait(false);
      //await this._navigationService.NavigateToAsync<OrdersViewModel>(async (viewModel, controlToShow) =>
      //{
      //   customer.VmContentControl.Content = controlToShow;//.GetViewModel().VmContentControl;
      //   await viewModel.LoadAsync(new object[] { customer.CompanyID });
      //});
   }
}
