using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Controls;
using Snijderman.Common.Mvvm;
using Snijderman.Common.Mvvm.Services;
using Snijderman.Samples.Common.Model;
using Snijderman.Samples.Common.Services;

namespace Snijderman.Wpf.MVVM.Example.ViewModels
{
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

      public override async Task LoadAsync() => this._customers = new ObservableCollection<CustomerViewModel>(this.GetCustomers(await this._customerService.GetCustomers()));
      private IEnumerable<CustomerViewModel> GetCustomers(IEnumerable<Customer> customers)
      {
         foreach (var customer in customers)
         {
            yield return new CustomerViewModel
            {
               CompanyID = customer.CompanyID,
               CompanyName = customer.CompanyName,
               Orders = customer.Orders
            };
         }
      }

      private ObservableCollection<CustomerViewModel> _customers;
      public ObservableCollection<CustomerViewModel> Customers
      {
         get => this._customers;
         set => this.Set(ref this._customers, value);
      }
      public ContentControl VmContentControl { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

      private async Task LoadCustomerOrders(CustomerViewModel customer)
      {
         if (customer == default)
         {
            this._messageService.Send(this, MessageConstants.StatusMessage, $"No customer selected");
            return;
         }

         this._messageService.Send(this, MessageConstants.StatusMessage, $"Customer '{customer.CompanyID}' selected");

         await this._navigationService.NavigateToAsync<OrdersViewModel>(async (viewModel, controlToShow) =>
         {
            customer.VmContentControl.Content = controlToShow;//.GetViewModel().VmContentControl;
            await viewModel.LoadAsync(new object[] { customer.CompanyID });
         });
      }
   }
}
