using System;
using System.Linq;
using System.Threading.Tasks;
using Snijderman.Common.Blazor.Components;
using Snijderman.Samples.Blazor.Mvvm.ViewModels;

namespace Snijderman.Samples.Blazor.Mvvm.Pages
{
   public partial class Dashboard : MvvmViewModelComponentBase<DashboardViewModel>
   {
      protected override async Task<bool> ExecuteViewModelLoadAsync(bool firstRender)
      {
         if (!firstRender)
         {
            return false;
         }

         await this.ViewModel.LoadAsync();
         if (this.ViewModel.Customers?.Count > 0)
         {
            this.ViewModel.Selected = this.ViewModel.Customers.FirstOrDefault();
         }
         return true;
      }

      //protected override async Task OnAfterRenderAsync(bool firstRender)
      //{
      //   if (firstRender)
      //   {
      //      this.SetBindingContext();
      //      //await Task.Delay(TimeSpan.FromSeconds(3));
      //      await this.ViewModel.LoadAsync();
      //      if (this.ViewModel.Customers?.Count > 0)
      //      {
      //         this.ViewModel.Selected = this.ViewModel.Customers.FirstOrDefault();
      //      }
      //      this.StateHasChanged();
      //   }
      //   await base.OnAfterRenderAsync(firstRender);
      //}
   }
}
