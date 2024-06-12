using System;
using System.Threading.Tasks;
using Snijderman.Common.Mvvm;
using Snijderman.Common.Mvvm.Services;
using Snijderman.Wpf.MVVM.Example.ViewModels;

namespace Snijderman.Wpf.MVVM.Example.Services;

public class ApplicationHostService : Common.Wpf.Mvvm.Services.ApplicationHostService
{
   private readonly INavigationService _navigationService;
   private readonly IMessageService _messageService;

   public ApplicationHostService(IServiceProvider serviceProvider, INavigationService navigationService, IMessageService messageService) : base(serviceProvider)
   {
      this._navigationService = navigationService;
      this._messageService = messageService;
   }

   public override async Task HandleActivationAsync()
   {
      await base.HandleActivationAsync().ConfigureAwait(false);

      if (this.ShellWindow is IWpfMvvmControl viewControl)
      {
         await this._navigationService.NavigateToAsync<CustomersViewModel>(async (viewModel, controlToShow) =>
         {
            var contentControl = viewControl.GetViewModel().VmContentControl;
            contentControl.Content = controlToShow;
            await (viewModel?.LoadAsync()).ConfigureAwait(false);
         }).ConfigureAwait(false);
         this._messageService.Send(this, MessageConstants.DisplayWaiting, false);
      }
   }
}
