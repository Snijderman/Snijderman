using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Snijderman.Common.Wpf.Mvvm.Services;

namespace Snijderman.Common.Wpf;

public abstract class SnijdermanWpfApplication : Application
{
   public IHost Host { get; private set; }

   public IConfiguration Configuration { get; private set; }

   public SnijdermanWpfApplication()
   {
      this.SetupUnhandledExceptionHandling();
   }

   public T GetService<T>() where T : class => this.Host.Services.GetService(typeof(T)) as T;

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

   protected override async void OnStartup(StartupEventArgs e)
   {
      this.LoadResources();
      await this.InitializeApplicationAsync(e).ConfigureAwait(false);
   }

   private void LoadResources()
   {
      var foo = new Uri("pack://application:,,,/Snijderman.Common.Wpf;component/Themes/Dark.xaml", UriKind.RelativeOrAbsolute);
      Current.Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = foo });
      return;

      //var assembly = Assembly.GetAssembly(typeof(Common.Wpf.Themes.Default.AppearanceManager));
      //var manifestEntries = assembly.GetManifestResourceNames().ToList();
      //for (var i = 0; i < manifestEntries.Count; i++)
      //{
      //}

      //var resource = this.GetResourceDictionary(assembly);
      //using (var stream = assembly.GetManifestResourceStream("Texts.en-GB.xaml"))
      //{
      //   using (XmlReader reader = new XmlTextReader(stream))
      //   {
      //      ResourceDictionary dic = (ResourceDictionary)XamlReader.Load(reader);
      //   }
      //}
      //this.Resources.MergedDictionaries.Add(XamlReader.Load)
   }

   //public ResourceDictionary GetResourceDictionary(Assembly assembly)
   //{
   //   var stream = assembly.GetManifestResourceStream(assembly.GetName().Name + ".g.resources");
   //   using (var reader = new ResourceReader(stream))
   //   {
   //      foreach (DictionaryEntry entry in reader)
   //      {
   //         //Debug.WriteLine($"Resource key: {entry.Key}");
   //         //if (!entry.Key.ToString().EndsWith("Themes/Dark.baml", StringComparison.OrdinalIgnoreCase))
   //         //{
   //         //   continue;
   //         //}
   //         var readStream = entry.Value as Stream;
   //         var bamlReader = new Baml2006Reader(readStream);
   //         var loadedObject = System.Windows.Markup.XamlReader.Load(bamlReader) as ResourceDictionary;
   //         if (loadedObject != null)
   //         {
   //            Current.Resources.MergedDictionaries.Add(loadedObject);
   //         }
   //      }
   //   }
   //   return null;
   //}

   private async Task InitializeApplicationAsync(StartupEventArgs e)
   {
      try
      {
         var appLocation = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

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

   private void ConfigureServices(HostBuilderContext context, IServiceCollection services)
   {
      this.Configuration = context.Configuration;

      // register own default/required services
      services.AddTransient<IDialogService, DialogService>();

      // App Host
      services.AddHostedService<ApplicationHostService>();

      // let the application do it's initialization
      this.Initialize(context, services);
   }

   protected abstract void Initialize(HostBuilderContext context, IServiceCollection services);

   protected override async void OnExit(ExitEventArgs e)
   {
      if (this.Host == null)
      {
         return;
      }

      await this.Host.StopAsync().ConfigureAwait(false);
      this.Host.Dispose();
      this.Host = null;
   }
}
