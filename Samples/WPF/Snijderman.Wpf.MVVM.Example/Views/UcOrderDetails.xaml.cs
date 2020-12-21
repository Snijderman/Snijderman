using System.Windows.Controls;
using Snijderman.Common.Mvvm;
using Snijderman.Wpf.MVVM.Example.ViewModels;

namespace Snijderman.Wpf.MVVM.Example.Views
{
   /// <summary>
   /// Interaction logic for UcOrderDetails.xaml
   /// </summary>
   public partial class UcOrderDetails : UserControl, IMvvmControl<OrderDetailsViewModel>
   {
      public UcOrderDetails(OrderDetailsViewModel viewModel)
      {
         this.InitializeComponent();
         this.DataContext = viewModel;
      }

      public OrderDetailsViewModel GetViewModel() => this.DataContext as OrderDetailsViewModel;
   }
}
