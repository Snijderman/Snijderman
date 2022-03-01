using System.Windows;
using System.Windows.Controls;

namespace Snijderman.Common.Wpf.Controls;

/// <summary>
/// Represents a control that indicates that an operation is ongoing. 
/// </summary>
[TemplateVisualState(GroupName = GroupActiveStates, Name = StateInactive)]
[TemplateVisualState(GroupName = GroupActiveStates, Name = StateActive)]
public class ModernProgress : Control
{
   private const string GroupActiveStates = "ActiveStates";
   private const string StateInactive = "Inactive";
   private const string StateActive = "Active";

   /// <summary>
   /// Identifies the IsActive property.
   /// </summary>
   public static readonly DependencyProperty IsActiveProperty = DependencyProperty.Register("IsActive", typeof(bool), typeof(ModernProgress), new PropertyMetadata(false, OnIsActiveChanged));

   /// <summary>
   /// Initializes a new instance of the <see cref="ModernProgress"/> class.
   /// </summary>
   public ModernProgress()
   {
      this.DefaultStyleKey = typeof(ModernProgress);
   }

   private void GotoCurrentState(bool animate)
   {
      var state = this.IsActive ? StateActive : StateInactive;

      VisualStateManager.GoToState(this, state, animate);
   }

   /// <summary>
   /// When overridden in a derived class, is invoked whenever application code or internal processes call <see cref="M:System.Windows.FrameworkElement.ApplyTemplate" />.
   /// </summary>
   public override void OnApplyTemplate()
   {
      base.OnApplyTemplate();

      this.GotoCurrentState(false);
   }

   private static void OnIsActiveChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
   {
      ((ModernProgress)o).GotoCurrentState(true);
   }

   /// <summary>
   /// Gets or sets a value that indicates whether the <see cref="ModernProgress"/> is showing progress.
   /// </summary>
   public bool IsActive
   {
      get => (bool)this.GetValue(IsActiveProperty);
      set => this.SetValue(IsActiveProperty, value);
   }
}
