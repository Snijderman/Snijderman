using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Snijderman.Common.Mvvm;

public abstract class ObservableObject : INotifyPropertyChanged
{
   #region Debugging Aides

   /// <summary>
   /// Warns the developer if this object does not have
   /// a public property with the specified name. This 
   /// method does not exist in a Release build.
   /// </summary>
   [Conditional("DEBUG")]
   [DebuggerStepThrough]
   public virtual void VerifyPropertyName(string propertyName)
   {
      // Verify that the property name matches a real,  
      // public, instance property on this object.
      if (TypeDescriptor.GetProperties(this)[propertyName] == null)
      {
         var msg = "Invalid property name: " + propertyName;

         if (this.ThrowOnInvalidPropertyName)
         {
            throw new Exception(msg);
         }

         Debug.Fail(msg);
      }
   }

   /// <summary>
   /// Returns whether an exception is thrown, or if a Debug.Fail() is used
   /// when an invalid property name is passed to the VerifyPropertyName method.
   /// The default value is false, but subclasses used by unit tests might 
   /// override this property's getter to return true.
   /// </summary>
   protected virtual bool ThrowOnInvalidPropertyName { get; private set; }

   #endregion // Debugging Aides

   #region INotifyPropertyChanged Members

   /// <summary>
   /// Raises the PropertyChange event for the property specified
   /// </summary>
   /// <param name="propertyName">Property name to update. Is case-sensitive.</param>
   public virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
   {
      this.VerifyPropertyName(propertyName);
      this.OnPropertyChanged(propertyName);
   }

   /// <summary>
   /// Raised when a property on this object has a new value.
   /// </summary>
   public event PropertyChangedEventHandler PropertyChanged;

   /// <summary>
   /// Raises this object's PropertyChanged event.
   /// </summary>
   /// <param name="propertyName">The property that has a new value.</param>
   protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
   {
      this.VerifyPropertyName(propertyName);

      this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
   }

   protected bool Set<T>(ref T field, T newValue = default, [CallerMemberName] string propertyName = null)
   {
      if (!EqualityComparer<T>.Default.Equals(field, newValue))
      {
         field = newValue;
         this.OnPropertyChanged(propertyName);
         return true;
      }
      return false;
   }


   #endregion // INotifyPropertyChanged Members
}
