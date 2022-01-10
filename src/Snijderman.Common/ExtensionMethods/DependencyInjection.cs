using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Snijderman.Common.Configuration;

namespace Snijderman.Common.ExtensionMethods
{
   public static class DependencyInjection
   {
      public static IServiceCollection AddAppSettings(this IServiceCollection services, IConfiguration configuration)
      {
         if (services == null)
         {
            throw new ArgumentNullException(nameof(services));
         }

         AppSettings ExtractAppSettingsFromConfig()
         {
            var appSettings = new AppSettings();
            var appSettingsFromConfig = configuration?.GetSection(nameof(AppSettings));
            if (appSettingsFromConfig == null)
            {
               return appSettings;
            }

            foreach (var settingFromConfig in appSettingsFromConfig.GetChildren())
            {
               var key = settingFromConfig.GetSection("Key").Value;
               if (string.IsNullOrWhiteSpace(key))
               {
                  continue;
               }

               appSettings.Settings.Add((key, settingFromConfig.GetSection("Value").Value));
            }

            return appSettings;
         }

         services.AddSingleton(ExtractAppSettingsFromConfig());

         return services;
      }
   }

}
