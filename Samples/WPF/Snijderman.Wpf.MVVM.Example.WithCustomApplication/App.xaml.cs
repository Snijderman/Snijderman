using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Snijderman.Common.Wpf;
using Snijderman.Common.Wpf.Mvvm.Views;

namespace Snijderman.Wpf.MVVM.Example.WithCustomApplication
{
   /// <summary>
   /// Interaction logic for App.xaml
   /// </summary>
   public partial class App : SnijdermanWpfApplication
   {
      protected override void Initialize(HostBuilderContext context, IServiceCollection services)
      {
         services.AddTransient<IShellWindow, MainWindow>();
      }
   }
}
