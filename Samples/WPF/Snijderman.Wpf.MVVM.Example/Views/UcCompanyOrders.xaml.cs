using System.Windows.Controls;
using Snijderman.Common.Mvvm;
using Snijderman.Wpf.MVVM.Example.ViewModels;

namespace Snijderman.Wpf.MVVM.Example.Views;

/// <summary>
/// Interaction logic for UcCompanyOrders.xaml
/// </summary>
public partial class UcCompanyOrders : UserControl, IMvvmControl<OrdersViewModel>
{
   public UcCompanyOrders(OrdersViewModel viewModel)
   {
      this.InitializeComponent();
      this.DataContext = viewModel;
   }

   public OrdersViewModel GetViewModel() => this.DataContext as OrdersViewModel;
}
