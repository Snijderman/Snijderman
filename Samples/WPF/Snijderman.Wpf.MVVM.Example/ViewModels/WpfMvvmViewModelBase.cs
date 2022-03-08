using System.Windows.Controls;
using Snijderman.Common.Mvvm;
using Snijderman.Common.Wpf.Extensions;

namespace Snijderman.Wpf.MVVM.Example.ViewModels;

public abstract class WpfMvvmViewModelBase : ViewModelBase, IWpfMvvmViewModel
{
   public WpfMvvmViewModelBase()
   {
      this.VmContentControl = CreateContentControl();
   }

   public ContentControl VmContentControl { get; set; }
   private ContentControl CreateContentControl() => App.Current.InvokeIfRequired(() => new ContentControl());
}
