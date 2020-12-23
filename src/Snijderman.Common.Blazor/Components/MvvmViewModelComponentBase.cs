using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Snijderman.Common.Mvvm;

namespace Snijderman.Common.Blazor.Components
{
   public abstract class MvvmViewModelComponentBase<T> : MvvmComponentBase, IMvvmControl<T> where T : ViewModelBase
   {
      protected internal T ViewModel { get; set; }

      public virtual T GetViewModel() => this.ViewModel;

      protected internal TValue Bind<TValue>(Expression<Func<T, TValue>> property) => base.AddBinding(this.ViewModel, property);

      protected void SetBindingContext() => this.ViewModel ??= this.ServiceProvider.GetRequiredService<T>();

      protected override async Task OnAfterRenderAsync(bool firstRender)
      {
         await this.OnHandleViewModelLoadAsync(firstRender);
         await base.OnAfterRenderAsync(firstRender);
      }

      protected virtual async Task OnHandleViewModelLoadAsync(bool firstRender)
      {
         // by default, only execute view model load on the first time rendering
         if (!firstRender)
         {
            return;
         }

         this.SetBindingContext();
         if (await this.ExecuteViewModelLoadAsync(firstRender))
         {
            await this.InvokeAsync(this.StateHasChanged);
         }
      }

      /// <summary>
      /// Handle loading the viewmodel
      /// </summary>
      /// <param name="firstRender"></param>
      /// <returns>A boolean, indicating the state of the viewmodel has changed</returns>
      protected abstract Task<bool> ExecuteViewModelLoadAsync(bool firstRender);
   }
}
