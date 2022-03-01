using System.ComponentModel;

namespace Snijderman.Common.Wpf.Themes.Default;

public abstract class NotifyPropertyChanged : INotifyPropertyChanged
{
   /// <summary>
   /// Occurs when a property value changes.
   /// </summary>
   public event PropertyChangedEventHandler PropertyChanged;

   /// <summary>
   /// Raises the PropertyChanged event.
   /// </summary>
   /// <param name="propertyName">Name of the property.</param>
   protected virtual void OnPropertyChanged(string propertyName)
   {
      this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
   }
}
