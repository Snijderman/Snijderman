using Snijderman.Samples.Common.Model;

namespace Snijderman.Samples.Blazor.Mvvm.ViewModels;

internal static class Helper
{
   internal static void SetCustomerViewModelProperties(CustomerViewModel customerViewModel, Customer customer)
   {
      customerViewModel.CompanyId = customer.CompanyID;
      customerViewModel.CompanyName = customer.CompanyName;

   }

   internal static void SetOrderViewModelProperties(OrderViewModel orderViewModel, Order order)
   {
      orderViewModel.OrderId = order.OrderID;
      orderViewModel.OrderDate = order.OrderDate;
      orderViewModel.RequiredDate = order.RequiredDate;
      orderViewModel.ShippedDate = order.ShippedDate;
      orderViewModel.ShipperName = order.ShipperName;
      orderViewModel.ShipperPhone = order.ShipperPhone;
      orderViewModel.Freight = order.Freight;
      orderViewModel.Company = order.Company;
      orderViewModel.ShipTo = order.ShipTo;
      orderViewModel.OrderTotal = order.OrderTotal;
      orderViewModel.Status = order.Status;
      orderViewModel.Symbol = order.Symbol;
      orderViewModel.SymbolCode = order.SymbolCode;
   }
}
