using System.Windows.Controls;
using Snijderman.Common.Mvvm;

namespace Snijderman.Wpf.MVVM.Example.ViewModels
{
   public interface IWpfMvvmViewModel : IMvvmViewModel
   {
      public ContentControl VmContentControl { get; set; }
   }
}
