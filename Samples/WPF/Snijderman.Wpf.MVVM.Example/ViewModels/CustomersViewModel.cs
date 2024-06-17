using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using Snijderman.Common.Mvvm;
using Snijderman.Common.Mvvm.Services;
using Snijderman.Samples.Common.Model;
using Snijderman.Samples.Common.Services;

namespace Snijderman.Wpf.MVVM.Example.ViewModels;

public class CustomersViewModel : ItemSelectedViewModelBase<CustomerViewModel>, IWpfMvvmViewModel
{
   private readonly ICustomerService _customerService;
   private readonly IMessageService _messageService;
   private readonly INavigationService _navigationService;

   public CustomersViewModel(ICustomerService customerService, IMessageService messageService, INavigationService navigationService)
   {
      this._customerService = customerService;
      this._messageService = messageService;
      this._navigationService = navigationService;
      this.SelectedItemChanged += this.LoadCustomerOrders;
   }

   public override async Task LoadAsync() => this.Customers = new ReadOnlyCollection<CustomerViewModel>(this.GetCustomers(await this._customerService.GetCustomersAsync().ConfigureAwait(false)).ToList());
   private IEnumerable<CustomerViewModel> GetCustomers(IEnumerable<Customer> customers)
   {
      foreach (var customer in customers)
      {
         yield return new CustomerViewModel
         {
            CompanyId = customer.CompanyId,
            CompanyName = customer.CompanyName,
            Orders = customer.Orders
         };
      }
   }

   private ReadOnlyCollection<CustomerViewModel> _customers;
   public ReadOnlyCollection<CustomerViewModel> Customers
   {
      get => this._customers;
      set => this.Set(ref this._customers, value);
   }

   public ContentControl VmContentControl { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

   private async Task LoadCustomerOrders(CustomerViewModel customer)
   {
      if (customer == default)
      {
         this._messageService.Send(this, MessageConstants.StatusMessage, "No customer selected");
         return;
      }

      this._messageService.Send(this, MessageConstants.DisplayWaiting, true);

      this._messageService.Send(this, MessageConstants.StatusMessage, $"Customer '{customer.CompanyId}' selected");

      try
      {
         await this._navigationService.NavigateToAsync<OrdersViewModel>(async (viewModel, controlToShow) =>
         {
            customer.VmContentControl.Content = controlToShow;
            await viewModel.LoadAsync([customer.CompanyId]).ConfigureAwait(false);
         }).ConfigureAwait(false);
      }
      catch (Exception exc)
      {
         Console.WriteLine(exc);
         this._messageService.Send(this, MessageConstants.StatusMessage, $"An error occurred: {exc.Message}");
      }

      this._messageService.Send(this, MessageConstants.DisplayWaiting, false);
   }
}
