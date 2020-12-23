using System.ComponentModel;
using System.Reflection;
using Snijderman.Common.Blazor.Internal.WeakEventListener;

namespace Snijderman.Common.Blazor.Internal.Bindings
{
   public interface IBindingFactory
   {
      IBinding Create(INotifyPropertyChanged source, PropertyInfo propertyInfo, IWeakEventManager weakEventManager);
   }

   internal class BindingFactory : IBindingFactory
   {
      public IBinding Create(INotifyPropertyChanged source, PropertyInfo propertyInfo, IWeakEventManager weakEventManager) => new Binding(source, propertyInfo, weakEventManager);
   }
}
