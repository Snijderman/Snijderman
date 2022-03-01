using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.Extensions.DependencyInjection;
using Snijderman.Common.Mvvm;

namespace Snijderman.Common.Blazor.Components;

public abstract class MvvmViewModelComponentBase<T> : MvvmComponentBase, IMvvmControl<T> where T : ViewModelBase
{
   private T _viewModel;
   protected internal T ViewModel
   {
      get => this._viewModel;
      set
      {
         if (this.ViewModel != default && !EqualityComparer<T>.Default.Equals(this._viewModel, value))
         {
            this.ViewModel.PropertyChanged -= this.HandleStateChanges;
         }
         this._viewModel = value;
         this.ViewModel.PropertyChanged += this.HandleStateChanges;
      }
   }

   public virtual T GetViewModel() => this.ViewModel;

   protected internal TValue Bind<TValue>(Expression<Func<T, TValue>> property) => base.AddBinding(this.ViewModel, property);

   protected void SetBindingContext()
   {
      this.ViewModel ??= this.ServiceProvider.GetRequiredService<T>();
   }

   protected override void Dispose(bool disposing)
   {
      if (disposing)
      {
         this.ViewModel.PropertyChanged -= this.HandleStateChanges;
         base.Dispose(disposing);
      }
   }
}
