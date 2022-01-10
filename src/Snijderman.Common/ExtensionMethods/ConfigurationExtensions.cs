using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting.WindowsServices;
using Snijderman.Common.Environment;

namespace Snijderman.Common.ExtensionMethods
{
   public static class ConfigurationExtensions
   {
      public static int GetConfigOrDefaultSetting(this IConfiguration configuration, string configKey, int defaultValue)
      {
         return int.TryParse(configuration?[configKey], out var value) ? value : defaultValue;
      }

      public static IConfigurationBuilder BuildAppConfiguration(this IConfigurationBuilder configurationBuilder, IEnvironmentService environmentService = null)
      {
         if (configurationBuilder is null)
         {
            throw new System.ArgumentNullException(nameof(configurationBuilder));
         }

         if (!IsWindowsService())
         {
            configurationBuilder.SetBasePath(Directory.GetCurrentDirectory());
         }

         configurationBuilder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
         if (environmentService != null)
         {
            configurationBuilder.AddJsonFile($"appsettings.{environmentService.EnvironmentName}.json", optional: true, reloadOnChange: true);
         }

         return configurationBuilder.AddEnvironmentVariables();
      }

      private static bool IsWindowsService(IEnvironmentService environmentService = null)
      {
         return environmentService == null ? WindowsServiceHelpers.IsWindowsService() : environmentService.IsWindowsService;
      }
   }
}
