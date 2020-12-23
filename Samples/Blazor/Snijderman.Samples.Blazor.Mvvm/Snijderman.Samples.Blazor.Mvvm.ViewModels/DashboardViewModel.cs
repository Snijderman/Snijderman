using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Snijderman.Common.Mvvm;
using Snijderman.Common.Mvvm.Services;
using Snijderman.Samples.Common.Model;
using Snijderman.Samples.Common.Services;

namespace Snijderman.Samples.Blazor.Mvvm.ViewModels
{
   public class DashboardViewModel : ItemSelectedViewModelBase<CustomerViewModel>
   {
      private readonly ICustomerService _customerService;
      private readonly IMessageService _messageService;
      private readonly INavigationService _navigationService;
      private readonly IServiceProvider _serviceProvider;

      public DashboardViewModel(ICustomerService customerService, IMessageService messageService, INavigationService navigationService, IServiceProvider serviceProvider)
      {
         this._customerService = customerService;
         this._messageService = messageService;
         this._navigationService = navigationService;
         this._serviceProvider = serviceProvider;
         this.SelectedItemChanged += this.LoadCustomerOrders;
      }

      public override async Task LoadAsync() => this._customers = new ObservableCollection<CustomerViewModel>(this.GetCustomers(await this._customerService.GetCustomers()));
      private IEnumerable<CustomerViewModel> GetCustomers(IEnumerable<Customer> customers)
      {
         foreach (var customer in customers)
         {
            yield return this.CreateCustomerViewModel(customer.CompanyID, customer.CompanyName);
         }
      }

      private CustomerViewModel CreateCustomerViewModel(string companyId, string companyName)
      {
         var customer = this._serviceProvider.GetRequiredService<CustomerViewModel>();
         customer.CompanyID = companyId;
         customer.CompanyName = companyName;
         return customer;
      }

      private ObservableCollection<CustomerViewModel> _customers;
      public ObservableCollection<CustomerViewModel> Customers
      {
         get => this._customers;
         set => this.Set(ref this._customers, value);
      }

      protected override void OnSelectedItemChanged(CustomerViewModel selectedItem)
      {
         this.SelectedItem = selectedItem?.CompanyID;
         foreach (var customer in this.Customers)
         {
            customer.IsSelected = customer.CompanyID == selectedItem?.CompanyID;
         }
         base.OnSelectedItemChanged(selectedItem);
      }

      private string _selectedItem;
      public string SelectedItem
      {
         get => this._selectedItem;
         set => this.Set(ref this._selectedItem, value);
      }

      public void SetSelectedTab(string id) => this.Selected = this.Customers.FirstOrDefault(x => string.Equals(x.CompanyID, id, StringComparison.OrdinalIgnoreCase));

      private async Task LoadCustomerOrders(CustomerViewModel customer)
      {
         if (customer == default)
         {
            this._messageService.Send(this, MessageConstants.StatusMessage, $"No customer selected");
            return;
         }

         this._messageService.Send(this, MessageConstants.StatusMessage, $"Customer '{customer.CompanyID}' selected");
         await Task.CompletedTask;
         //await this._navigationService.NavigateToAsync<OrdersViewModel>(async (viewModel, controlToShow) =>
         //{
         //   customer.VmContentControl.Content = controlToShow;//.GetViewModel().VmContentControl;
         //   await viewModel.LoadAsync(new object[] { customer.CompanyID });
         //});
      }
   }
}
