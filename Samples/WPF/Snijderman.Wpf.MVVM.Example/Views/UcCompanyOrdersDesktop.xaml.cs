using System.Windows.Controls;
using Snijderman.Common.Mvvm;
using Snijderman.Wpf.MVVM.Example.ViewModels;

namespace Snijderman.Wpf.MVVM.Example.Views
{
   /// <summary>
   /// Interaction logic for UcCompanyOrdersDesktop.xaml
   /// </summary>
   public partial class UcCompanyOrdersDesktop : UserControl, IMvvmControl<CustomersViewModel>
   {
      public UcCompanyOrdersDesktop(CustomersViewModel viewModel)
      {
         this.InitializeComponent();
         this.DataContext = viewModel;
      }

      public CustomersViewModel GetViewModel() => this.DataContext as CustomersViewModel;
   }
}
