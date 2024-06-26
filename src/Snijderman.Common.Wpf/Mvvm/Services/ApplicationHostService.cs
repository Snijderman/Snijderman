using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.Hosting;
using Snijderman.Common.Wpf.Mvvm.Views;

namespace Snijderman.Common.Wpf.Mvvm.Services;

public class ApplicationHostService : IHostedService
{
   protected readonly IServiceProvider ServiceProvider;
   protected IShellWindow ShellWindow;

   public ApplicationHostService(IServiceProvider serviceProvider)
   {
      this.ServiceProvider = serviceProvider;
   }

   public virtual async Task StartAsync(CancellationToken cancellationToken)
   {
      // Initialize services that you need before app activation
      await this.InitializeAsync().ConfigureAwait(false);

      await this.HandleActivationAsync().ConfigureAwait(false);

      // Tasks after activation
      await this.StartupAsync().ConfigureAwait(false);
   }

   public virtual async Task StopAsync(CancellationToken cancellationToken) => await Task.CompletedTask.ConfigureAwait(false);

   protected virtual async Task InitializeAsync() => await Task.CompletedTask.ConfigureAwait(false);

   protected virtual async Task HandleActivationAsync()
   {
      if (!Application.Current.Windows.OfType<IShellWindow>().Any())
      {
         // Default activation that navigates to the apps default page
         this.ShellWindow = this.ServiceProvider.GetService(typeof(IShellWindow)) as IShellWindow;
         if (this.ShellWindow == default)
         {
            throw new InvalidOperationException("No window found that implements type 'IShellWindow'");
         }

         this.ShellWindow.ShowWindow();

         await Task.CompletedTask.ConfigureAwait(false);
      }
   }

   protected virtual async Task StartupAsync() => await Task.CompletedTask.ConfigureAwait(false);
}
