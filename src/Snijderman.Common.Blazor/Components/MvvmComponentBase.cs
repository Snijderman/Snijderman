using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Snijderman.Common.Blazor.Internal.Bindings;
using Snijderman.Common.Blazor.Internal.WeakEventListener;
using Snijderman.Common.Mvvm;

namespace Snijderman.Common.Blazor.Components;

public abstract class MvvmComponentBase : ComponentBase, IDisposable
{
   private readonly HashSet<IBinding> _bindings = new();
   private IBindingFactory _bindingFactory;
   private IWeakEventManager _weakEventManager;
   private IWeakEventManagerFactory _weakEventManagerFactory;
   private readonly Guid _id = Guid.NewGuid();

   protected Guid Id => this._id;

   [Inject] protected IServiceProvider ServiceProvider { get; set; }

   protected override void OnInitialized() => this.InitializeDependencies();

   private void InitializeDependencies()
   {
      this._weakEventManagerFactory = this.ServiceProvider.GetRequiredService<IWeakEventManagerFactory>();
      this._bindingFactory = this.ServiceProvider.GetRequiredService<IBindingFactory>();
      this._weakEventManager = this._weakEventManagerFactory.Create();
   }

   protected void HandleStateChanges(object sender, EventArgs e) => this.InvokeAsync(this.StateHasChanged);

   internal virtual void BindingOnBindingValueChanged(object sender, EventArgs e) => this.HandleStateChanges(sender, e);

   protected internal TValue Bind<TViewModel, TValue>(TViewModel viewModel, Expression<Func<TViewModel, TValue>> property) where TViewModel : ViewModelBase => this.AddBinding(viewModel, property);

   public virtual TValue AddBinding<TViewModel, TValue>(TViewModel viewModel, Expression<Func<TViewModel, TValue>> propertyExpression) where TViewModel : ViewModelBase
   {
      var propertyInfo = ValidateAndResolveBindingContext(viewModel, propertyExpression);

      var binding = this._bindingFactory.Create(viewModel, propertyInfo, this._weakEventManagerFactory.Create());
      if (this._bindings.Contains(binding))
      {
         return (TValue)binding.GetValue();
      }

      this._weakEventManager.AddWeakEventListener<IBinding, EventArgs>(binding, nameof(Binding.BindingValueChanged), this.BindingOnBindingValueChanged);
      binding.Initialize();

      this._bindings.Add(binding);

      return (TValue)binding.GetValue();
   }

   protected static PropertyInfo ValidateAndResolveBindingContext<TViewModel, TValue>(TViewModel viewModel, Expression<Func<TViewModel, TValue>> property)
   {
      if (viewModel is null)
      {
         throw new BindingException("ViewModelType is null");
      }

      if (property is null)
      {
         throw new BindingException("Property expression is null");
      }

      if (property.Body is not MemberExpression m)
      {
         throw new BindingException("Binding member needs to be a property");
      }

      if (m.Member is not PropertyInfo p)
      {
         throw new BindingException("Binding member needs to be a property");
      }

      if (typeof(TViewModel).GetProperty(p.Name) is null)
      {
         throw new BindingException($"Cannot find property {p.Name} in type {viewModel.GetType().FullName}");
      }

      return p;
   }

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
         foreach (var binding in this._bindings)
         {
            this._weakEventManager.RemoveWeakEventListener(binding);
            binding.Dispose();
         }
      }
   }

   ~MvvmComponentBase()
   {
      this.Dispose(false);
   }

   #endregion
}
