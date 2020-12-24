using System;
using System.Linq;
using System.Threading.Tasks;
using Snijderman.Common.Blazor.Components;
using Snijderman.Samples.Blazor.Mvvm.ViewModels;

namespace Snijderman.Samples.Blazor.Mvvm.Pages
{
   public partial class Dashboard : MvvmViewModelComponentBase<DashboardViewModel>
   {
      protected override async Task OnAfterRenderAsync(bool firstRender)
      {
         if (firstRender)
         {
            this.SetBindingContext();
            await Task.Delay(TimeSpan.FromSeconds(1.5));
            await this.ViewModel.LoadAsync();
            if (this.ViewModel.Customers?.Count > 0)
            {
               this.ViewModel.Selected = this.ViewModel.Customers.FirstOrDefault();
            }
         }
         await base.OnAfterRenderAsync(firstRender);
      }
   }
}
