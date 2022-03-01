using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Hosting.WindowsServices;

namespace Snijderman.Common.Helpers;

[Obsolete("Replaced by IEnvironmentService")]
public static class Environment
{
   //private static readonly IEnvironmentService = new EnvironmentService();
   public const string Development = "Development";
   public const string Azure = "Azure";
   public const string Docker = "Docker";
   public const string UnitTesting = "UnitTesting";
   public const string FunctionalTesting = "FunctionalTesting";
   private const string EnvironmentVariableName = "CUSTOM_ENVIRONMENT";
   private static readonly List<string> _knownEnvironments = new()
   {
      EnvironmentVariableName,
      "ASPNETCORE_ENVIRONMENT",
      "DOTNET_ENVIRONMENT",
      "AZURE_FUNCTIONS_ENVIRONMENT"
   };

   public static string EnvironmentName
   {
      get
      {
         for (var i = 0; i < _knownEnvironments.Count; i++)
         {
            var env = System.Environment.GetEnvironmentVariable(_knownEnvironments[i]);
            if (!string.IsNullOrWhiteSpace(env))
            {
               return env;
            }
         }

         return default;
      }
   }

   public static void SetEnvironment(string environmentName) => System.Environment.SetEnvironmentVariable(EnvironmentVariableName, environmentName);

   public static bool IsDevelopEnvironment => string.Equals(Development, EnvironmentName, StringComparison.OrdinalIgnoreCase);

   public static bool IsAzureEnvironment => string.Equals(Azure, EnvironmentName, StringComparison.OrdinalIgnoreCase);

   public static bool IsDockerEnvironment => string.Equals(Docker, EnvironmentName, StringComparison.OrdinalIgnoreCase);

   public static bool IsUnitTestingEnvironment => string.Equals(UnitTesting, EnvironmentName, StringComparison.OrdinalIgnoreCase);

   public static bool IsFunctionalTestingEnvironment => string.Equals(FunctionalTesting, EnvironmentName, StringComparison.OrdinalIgnoreCase);

   public static StringBuilder ListEnvironmentVariables()
   {
      var variables = System.Environment.GetEnvironmentVariables();

      var sb = new StringBuilder();
      foreach (DictionaryEntry variable in variables)
      {
         sb.AppendLine($"{variable.Key} - {variable.Value}");
      }

      return sb;
   }

   public static bool IsWindowsService => WindowsServiceHelpers.IsWindowsService();
}
