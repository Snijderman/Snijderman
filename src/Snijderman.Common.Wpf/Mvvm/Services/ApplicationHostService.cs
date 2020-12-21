using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.Hosting;
using Snijderman.Common.Wpf.Mvvm.Views;

namespace Snijderman.Common.Wpf.Mvvm.Services
{
   public class ApplicationHostService : IHostedService
   {
      protected readonly IServiceProvider _serviceProvider;
      protected IShellWindow _shellWindow;

      public ApplicationHostService(IServiceProvider serviceProvider)
      {
         this._serviceProvider = serviceProvider;
      }

      public virtual async Task StartAsync(CancellationToken cancellationToken)
      {
         // Initialize services that you need before app activation
         await this.InitializeAsync();

         await this.HandleActivationAsync();

         // Tasks after activation
         await this.StartupAsync();
      }

      public virtual async Task StopAsync(CancellationToken cancellationToken) => await Task.CompletedTask;

      public virtual async Task InitializeAsync() => await Task.CompletedTask;

      public virtual async Task StartupAsync() => await Task.CompletedTask;

      public virtual async Task HandleActivationAsync()
      {
         if (!Application.Current.Windows.OfType<IShellWindow>().Any())
         {
            // Default activation that navigates to the apps default page
            this._shellWindow = this._serviceProvider.GetService(typeof(IShellWindow)) as IShellWindow;
            if (this._shellWindow == default)
            {
               throw new InvalidOperationException("No window found that implements type 'IShellWindow'");
            }

            this._shellWindow.ShowWindow();

            await Task.CompletedTask;
         }
      }
   }
}
