using System;
using Microsoft.Extensions.DependencyInjection;
using Snijderman.Common.Mvvm;

namespace Snijderman.Common.Wpf.Mvvm.Services;

public static class ExtensionMethods
{
   public static void ShowExceptionDialog(this IServiceProvider serviceProvider, Exception exception, string source)
   {
      var dialogService = serviceProvider.GetRequiredService<IDialogService>();
      // we need a UI control to know if we should dispatch
      var navigationService = serviceProvider.GetRequiredService<INavigationService>();
      dialogService.ShowExceptionDialog(exception, source);
      //if (navigationService.CurrentContentControl == default)
      //{
      //   dialogService.ShowExceptionDialog(exception, source);
      //}
      //else
      //{
      //   navigationService.CurrentContentControl.InvokeIfRequired(() => dialogService.ShowExceptionDialog(exception, source));
      //}
   }

   public static void ShowExceptionDialog(this IDialogService dialogService, Exception exception, string source) => dialogService.Show(source, $"An error occured:\r\n{exception}");
}
