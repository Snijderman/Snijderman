using System.Windows;
using System.Windows.Controls;

namespace Snijderman.Common.Wpf.Controls;

/// <summary>
/// Interaction logic for UcWaiting.xaml
/// </summary>
public partial class UcWaiting : UserControl
{
   public UcWaiting()
   {
      this.InitializeComponent();

      this.WaitingText = "Waiting...";
   }

   // Dependency properties declaration
   public static readonly DependencyProperty WaitingProperty = DependencyProperty.Register("WaitingText", typeof(string), typeof(UcWaiting), new PropertyMetadata(string.Empty, OnWaitingChanged));

   public static void OnWaitingChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
   {
      if (sender is UcWaiting waiting)
      {
         waiting.UiWaitingTextTxtbl.Text = $"{e.NewValue}";
      }
   }

   public string WaitingText
   {
      get => this.GetValue(WaitingProperty) as string;
      set => this.SetValue(WaitingProperty, value);
   }
}
