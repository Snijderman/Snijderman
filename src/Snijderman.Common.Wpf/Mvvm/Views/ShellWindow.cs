using Snijderman.Common.Wpf.Themes.Default.Controls;

namespace Snijderman.Common.Wpf.Mvvm.Views;

public class ShellWindow : ThemedWindow, IShellWindow
{
   public void CloseWindow() => this.Close();

   public void ShowWindow() => this.Show();
}
