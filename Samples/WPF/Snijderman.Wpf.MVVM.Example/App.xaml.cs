using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Snijderman.Common.Mvvm;
using Snijderman.Common.Mvvm.Services;
using Snijderman.Common.Wpf;
using Snijderman.Common.Wpf.Mvvm.Services;
using Snijderman.Common.Wpf.Mvvm.Views;
using Snijderman.Samples.Common.ExtensionMethods;
using Snijderman.Wpf.MVVM.Example.Services;
using Snijderman.Wpf.MVVM.Example.ViewModels;
using Snijderman.Wpf.MVVM.Example.Views;
using ApplicationHostService = Snijderman.Wpf.MVVM.Example.Services.ApplicationHostService;

namespace Snijderman.Wpf.MVVM.Example;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
   public IHost Host { get; private set; }

   public T GetService<T>() where T : class => this.Host.Services.GetService(typeof(T)) as T;

   public App()
   {
      this.SetupUnhandledExceptionHandling();
   }

   private async void OnStartup(object sender, StartupEventArgs e)
   {
      try
      {
         var appLocation = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

         // For more information about .NET generic host see  https://docs.microsoft.com/aspnet/core/fundamentals/host/generic-host?view=aspnetcore-3.0
         this.Host = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(e.Args)
                              .ConfigureAppConfiguration(c =>
                              {
                                 c.SetBasePath(appLocation);
                              })
                              .ConfigureServices(this.ConfigureServices)
                              .Build();

         await this.Host.StartAsync().ConfigureAwait(false);
      }
      catch (Exception exc)
      {
         Debug.WriteLine($"An error occured:\r\n{exc}");
         throw;
      }
   }

   private void SetupUnhandledExceptionHandling()
   {
      AppDomain.CurrentDomain.UnhandledException += (s, e) => this.LogUnhandledException((Exception)e.ExceptionObject, "AppDomain.CurrentDomain.UnhandledException");

      DispatcherUnhandledException += (s, e) =>
      {
         this.LogUnhandledException(e.Exception, "Application.Current.DispatcherUnhandledException");
         e.Handled = true;
      };

      TaskScheduler.UnobservedTaskException += (s, e) =>
      {
         this.LogUnhandledException(e.Exception, "TaskScheduler.UnobservedTaskException");
         e.SetObserved();
      };
   }

   private void LogUnhandledException(Exception exception, string source)
   {
      if (this.Host?.Services != default)
      {
         this.Host.Services.ShowExceptionDialog(exception, source);
      }
      else
      {
         throw new Exception("An unhandled exception occured", exception);
      }
   }

   public IConfiguration Configuration { get; private set; }

   private void ConfigureServices(HostBuilderContext context, IServiceCollection services)
   {
      this.Configuration = context.Configuration;

      // App Host
      services.AddHostedService<ApplicationHostService>();
      //// Business services
      services.RegisterSampleCommonServices();
      //// register views
      services.AddTransient<IShellWindow, MainWindow>();
      services.AddTransient<ShellWindowViewModel>();
      //// register view models + views
      services.AddViewModelAndControl<CustomersViewModel, UcCompanyOrdersDesktop>();
      services.AddViewModelAndControl<OrdersViewModel, UcCompanyOrders>();
      services.AddViewModelAndControl<OrderDetailsViewModel, UcOrderDetails>();

      //// MVVM services
      services.AddSingleton<IMessageService, MessageService>();
      services.AddTransient<IDialogService, DialogService>();
      services.AddSingleton<IMvvmControlService, MvvmControlService>();
      services.AddSingleton<INavigationService, WpfNavigationService>();

      services.AddSingleton<IErrorHandler, ErrorHandler>();
   }

   private async void OnExit(object sender, ExitEventArgs e)
   {
      await (this.Host?.StopAsync()).ConfigureAwait(false);
      this.Host?.Dispose();
      this.Host = null;
   }
}
