using Microsoft.AspNetCore.Components;
using Snijderman.Common.Blazor.Components;
using Snijderman.Samples.Blazor.Mvvm.ViewModels;

namespace Snijderman.Samples.Blazor.Mvvm.Components
{
   public partial class MainTabContent : MvvmViewModelComponentBase<CustomerViewModel>
   {
      [Parameter]
      public CustomerViewModel ViewModelInput
      {
         get => this.ViewModel;
         set => this.ViewModel = value;
      }
   }
}
