using System.Windows.Controls;
using Snijderman.Common.Mvvm;

namespace Snijderman.Wpf.MVVM.Example.ViewModels
{
   public abstract class WpfMvvmViewModelBase : ViewModelBase, IWpfMvvmViewModel
   {
      public ContentControl VmContentControl { get; set; } = new ContentControl();
   }
}
