using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Snijderman.Common.Blazor.Components;
using Snijderman.Samples.Blazor.Mvvm.ViewModels;

namespace Snijderman.Samples.Blazor.Mvvm.Components
{
   public partial class MainTabContent : MvvmViewModelComponentBase<CustomerViewModel>
   {
      private readonly Guid _id = Guid.NewGuid();
      protected Guid Id => this._id;

      [Parameter]
      public CustomerViewModel ViewModelInput
      {
         get => this.ViewModel;
         set => this.ViewModel = value;
      }

      protected override Task<bool> ExecuteViewModelLoadAsync(bool firstRender)
      {
         this.ViewModel.OrdersLoaded += this.HandleOrdersLoadedEventAsync;
         return Task.FromResult(false);
      }

      private async Task HandleOrdersLoadedEventAsync()
      {
         this.ViewModel.OrdersLoaded += this.HandleOrdersLoadedEventAsync;
         await this.InvokeAsync(this.StateHasChanged);
      }
   }
}
