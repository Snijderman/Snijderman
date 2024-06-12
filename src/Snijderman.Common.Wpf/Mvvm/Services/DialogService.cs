using System.Windows;
using Snijderman.Common.Wpf.Controls;

namespace Snijderman.Common.Wpf.Mvvm.Services;

public class DialogService : IDialogService
{
   public void Show(string title, string content)
   {
      ModernDialog.ShowMessage(content, title, MessageBoxButton.OK);
   }
}
