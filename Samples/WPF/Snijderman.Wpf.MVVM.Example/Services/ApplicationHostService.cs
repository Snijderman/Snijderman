using System;
using System.Threading.Tasks;
using Snijderman.Common.Mvvm;
using Snijderman.Common.Wpf.Extensions;
using Snijderman.Wpf.MVVM.Example.ViewModels;

namespace Snijderman.Wpf.MVVM.Example.Services
{
   public class ApplicationHostService : Common.Wpf.Mvvm.Services.ApplicationHostService
   {
      private readonly INavigationService _navigationService;

      public ApplicationHostService(IServiceProvider serviceProvider, INavigationService navigationService) : base(serviceProvider)
      {
         this._navigationService = navigationService;
      }

      public override async Task HandleActivationAsync()
      {
         await base.HandleActivationAsync().ConfigureAwait(false);

         if (this._shellWindow is IWpfMvvmControl viewControl)
         {
            await this._navigationService.NavigateToAsync<CustomersViewModel>(async (viewModel, controlToShow) =>
            {
               var contentControl = viewControl.GetViewModel().VmContentControl;
               contentControl.Content = controlToShow;
               await (viewModel?.LoadAsync()).ConfigureAwait(false);
            }).ConfigureAwait(false);
         }
      }
   }
}
