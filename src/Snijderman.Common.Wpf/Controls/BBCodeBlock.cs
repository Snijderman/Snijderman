using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;

namespace Snijderman.Common.Wpf.Controls;

/// <summary>
/// A lighweight control for displaying small amounts of rich formatted BBCode content.
/// </summary>
[ContentProperty("BBCode")]
public class BbCodeBlock : TextBlock
{
   /// <summary>
   /// Identifies the BBCode dependency property.
   /// </summary>
   public static DependencyProperty BbCodeProperty = DependencyProperty.Register("BbCode", typeof(string), typeof(BbCodeBlock), new PropertyMetadata(OnBBCodeChanged));

   ///// <summary>
   ///// Identifies the LinkNavigator dependency property.
   ///// </summary>
   //public static DependencyProperty LinkNavigatorProperty = DependencyProperty.Register("LinkNavigator", typeof(ILinkNavigator), typeof(BBCodeBlock), new PropertyMetadata(new DefaultLinkNavigator(), OnLinkNavigatorChanged));

   private bool _dirty;

   /// <summary>
   /// Initializes a new instance of the <see cref="BbCodeBlock"/> class.
   /// </summary>
   public BbCodeBlock()
   {
      // ensures the implicit BBCodeBlock style is used
      this.DefaultStyleKey = typeof(BbCodeBlock);

      this.AddHandler(FrameworkContentElement.LoadedEvent, new RoutedEventHandler(this.OnLoaded));
   }

   private static void OnBBCodeChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
   {
      ((BbCodeBlock)o).UpdateDirty();
   }

   //private static void OnLinkNavigatorChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
   //{
   //   if (e.NewValue == null)
   //   {
   //      // null values disallowed
   //      throw new ArgumentNullException("LinkNavigator");
   //   }

   //    ((BBCodeBlock)o).UpdateDirty();
   //}

   private void OnLoaded(object o, EventArgs e)
   {
      this.Update();
   }

   private void UpdateDirty()
   {
      this._dirty = true;
      this.Update();
   }

   private void Update()
   {
      if (!this.IsLoaded || !this._dirty)
      {
         return;
      }

      var bbcode = this.BbCode;

      this.Inlines.Clear();

      if (!string.IsNullOrWhiteSpace(bbcode))
      {
         var inline = new Run { Text = bbcode };
         this.Inlines.Add(inline);
      }
      this._dirty = false;
   }

   //private void OnRequestNavigate(object sender, RequestNavigateEventArgs e)
   //{
   //   try
   //   {
   //      // perform navigation using the link navigator
   //      this.LinkNavigator.Navigate(e.Uri, this, e.Target);
   //   }
   //   catch (Exception error)
   //   {
   //      // display navigation failures
   //      ModernDialog.ShowMessage(error.Message, ModernUI.Resources.NavigationFailed, MessageBoxButton.OK);
   //   }
   //}

   /// <summary>
   /// Gets or sets the BB code.
   /// </summary>
   /// <value>The BB code.</value>
   public string BbCode
   {
      get => (string)this.GetValue(BbCodeProperty);
      set => this.SetValue(BbCodeProperty, value);
   }

   ///// <summary>
   ///// Gets or sets the link navigator.
   ///// </summary>
   ///// <value>The link navigator.</value>
   //public ILinkNavigator LinkNavigator
   //{
   //   get { return (ILinkNavigator)GetValue(LinkNavigatorProperty); }
   //   set { SetValue(LinkNavigatorProperty, value); }
   //}
}
