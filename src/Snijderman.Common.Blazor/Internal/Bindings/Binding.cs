using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Reflection;
using Snijderman.Common.Blazor.Internal.WeakEventListener;

namespace Snijderman.Common.Blazor.Internal.Bindings;

public interface IBinding : IDisposable
{
   INotifyPropertyChanged Source { get; }

   PropertyInfo PropertyInfo { get; }

   event EventHandler BindingValueChanged;

   void Initialize();

   object GetValue();
}

internal class Binding : IBinding
{
   private readonly IWeakEventManager _weakEventManager;
   private INotifyCollectionChanged _boundCollection;
   private bool _isCollection;

   public Binding(INotifyPropertyChanged source, PropertyInfo propertyInfo, IWeakEventManager weakEventManager)
   {
      this._weakEventManager = weakEventManager ?? throw new ArgumentNullException(nameof(weakEventManager));
      this.Source = source ?? throw new ArgumentNullException(nameof(source));
      this.PropertyInfo = propertyInfo ?? throw new ArgumentNullException(nameof(propertyInfo));
   }

   public INotifyPropertyChanged Source { get; }

   public PropertyInfo PropertyInfo { get; }

   public event EventHandler BindingValueChanged;

   public void Initialize()
   {
      this._isCollection = typeof(INotifyCollectionChanged).IsAssignableFrom(this.PropertyInfo.PropertyType);
      this._weakEventManager.AddWeakEventListener(this.Source, this.SourceOnPropertyChanged);
      this.AddCollectionBindings();
   }

   public object GetValue() => this.PropertyInfo.GetValue(this.Source, null);

   private void AddCollectionBindings()
   {
      if (!this._isCollection || this.GetValue() is not INotifyCollectionChanged collection)
      {
         return;
      }

      this._weakEventManager.AddWeakEventListener(collection, this.CollectionOnCollectionChanged);
      this._boundCollection = collection;
   }

   private void SourceOnPropertyChanged(object sender, PropertyChangedEventArgs e)
   {
      if (e.PropertyName is null)
      {
         BindingValueChanged?.Invoke(this, EventArgs.Empty);
         return;
      }

      // This should just listen to the bindings property
      if (e.PropertyName != this.PropertyInfo.Name)
      {
         return;
      }

      if (this._isCollection)
      {
         // If our binding is a collection binding we need to remove the event
         // and reinitialize the collection bindings
         if (this._boundCollection != null)
         {
            this._weakEventManager.RemoveWeakEventListener(this._boundCollection);
         }

         this.AddCollectionBindings();
      }


      BindingValueChanged?.Invoke(this, EventArgs.Empty);
   }

   private void CollectionOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e) => BindingValueChanged?.Invoke(this, EventArgs.Empty);

   #region IDisposable Support

   public void Dispose()
   {
      this.Dispose(true);
      GC.SuppressFinalize(this);
   }

   protected virtual void Dispose(bool disposing)
   {
      if (disposing)
      {
         if (this._boundCollection != null)
         {
            this._weakEventManager.RemoveWeakEventListener(this._boundCollection);
         }

         this._weakEventManager.RemoveWeakEventListener(this.Source);
      }
   }

   #endregion

   #region Base overrides

   public override string ToString() => $"{this.PropertyInfo?.DeclaringType?.Name}.{this.PropertyInfo?.Name}";

   public override bool Equals(object obj) => obj is Binding b && ReferenceEquals(b.Source, this.Source) && b.PropertyInfo.Name == this.PropertyInfo.Name;

   public override int GetHashCode()
   {
      var hash = 13;
      hash = hash * 7 + this.Source.GetHashCode();
      hash = hash * 7 + this.PropertyInfo.Name.GetHashCode(StringComparison.InvariantCulture);

      return hash;
   }

   #endregion
}
