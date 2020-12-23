using System;
using System.Net.Http;
using System.Threading.Tasks;
using Blazorise.Material;
using Blazorise;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Blazorise.Icons.Material;
using Snijderman.Samples.Blazor.Mvvm.ViewModels;
using Snijderman.Common.Blazor.Extensions;

namespace Snijderman.Samples.Blazor.Mvvm.Client
{
   public class Program
   {
      public static async Task Main(string[] args)
      {
         var builder = WebAssemblyHostBuilder.CreateDefault(args);

         builder.Services.AddBlazorise(options =>
                          {
                             options.ChangeTextOnKeyPress = true;
                          })
                          .AddMaterialProviders()
                          .AddMaterialIcons();

         builder.Services.AddSingleton(new HttpClient
         {
            BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
         });

         builder.Services.AddBlazorMvvm();
         builder.Services.AddTransient<ClockViewModel>();

         builder.RootComponents.Add<App>("#app");

         var host = builder.Build();

         host.Services.UseMaterialProviders()
                      .UseMaterialIcons();

         await host.RunAsync();
      }
   }
}
