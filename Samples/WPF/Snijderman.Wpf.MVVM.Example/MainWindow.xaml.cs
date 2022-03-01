using Snijderman.Common.Wpf.Mvvm.Views;
using Snijderman.Wpf.MVVM.Example.ViewModels;

namespace Snijderman.Wpf.MVVM.Example;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : ShellWindow, IWpfMvvmControl
{
   public MainWindow(ShellWindowViewModel viewModel)
   {
      this.InitializeComponent();
      viewModel.VmContentControl = this.UiMainContent;
      this.DataContext = viewModel;
   }

   public WpfMvvmViewModelBase GetViewModel() => this.DataContext as WpfMvvmViewModelBase;
}
