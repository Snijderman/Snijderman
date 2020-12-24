using Blazorise;
using Blazorise.Icons.Material;
using Blazorise.Material;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Snijderman.Common.Blazor.Extensions;
using Snijderman.Common.Mvvm;
using Snijderman.Common.Mvvm.Services;
using Snijderman.Samples.Blazor.Mvvm.ViewModels;
using Snijderman.Samples.Common.ExtensionMethods;

namespace Snijderman.Samples.Blazor.Mvvm.ServerHosted
{
   public class Startup
   {
      public Startup(IConfiguration configuration)
      {
         this.Configuration = configuration;
      }

      public IConfiguration Configuration { get; }

      // This method gets called by the runtime. Use this method to add services to the container.
      // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
      public void ConfigureServices(IServiceCollection services)
      {
         services.AddBlazorise(options =>
                  {
                     options.ChangeTextOnKeyPress = true;
                  })
                  .AddMaterialProviders()
                  .AddMaterialIcons();

         services.AddRazorPages();
         services.AddServerSideBlazor();

         services.AddBlazorMvvm();
         services.AddMvvmViewModels();
         services.RegisterSampleCommonServices();
         services.AddSingleton<IMessageService, MessageService>();
         services.AddSingleton<IMvvmControlService, MvvmControlService>();
         services.AddSingleton<INavigationService, NavigationService>();

         services.AddServerSideBlazor().AddHubOptions((o) =>
         {
            o.MaximumReceiveMessageSize = 1024 * 1024 * 100;
         });
      }

      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
      {
         if (env.IsDevelopment())
         {
            app.UseDeveloperExceptionPage();
         }
         else
         {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //app.UseHsts();
         }

         //app.UseHttpsRedirection();
         app.UseStaticFiles();

         app.UseRouting();

         app.ApplicationServices.UseMaterialProviders()
                                .UseMaterialIcons();

         // this is required to be here or otherwise the messages between server and client will be too large and
         // the connection will be lost.
         //app.UseSignalR( route => route.MapHub<ComponentHub>( ComponentHub.DefaultPath, o =>
         //{
         //    o.ApplicationMaxBufferSize = 1024 * 1024 * 100; // larger size
         //    o.TransportMaxBufferSize = 1024 * 1024 * 100; // larger size
         //} ) );

         app.UseEndpoints(endpoints =>
         {
            endpoints.MapBlazorHub();
            endpoints.MapFallbackToPage("/_Host");
         });
      }
   }
}
