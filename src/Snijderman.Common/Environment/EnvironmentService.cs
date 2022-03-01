using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.WindowsServices;

namespace Snijderman.Common.Environment;

public class EnvironmentService : IEnvironmentService
{
   private const string EnvironmentVariableName = "CUSTOM_ENVIRONMENT";
   private static readonly List<string> _knownEnvironments = new()
   {
      EnvironmentVariableName,
      "ASPNETCORE_ENVIRONMENT",
      "DOTNET_ENVIRONMENT",
      "AZURE_FUNCTIONS_ENVIRONMENT"
   };
   private readonly IHostEnvironment _environment;

   public EnvironmentService(IConfiguration configuration, IHostEnvironment environment)
   {
      this._environment = environment;

      var customEnvironment = configuration?[EnvironmentVariableName];
      if (!string.IsNullOrWhiteSpace(customEnvironment))
      {
         this.SetEnvironment(customEnvironment);
      }
   }

   public string EnvironmentName
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

         return this._environment.EnvironmentName;
      }
   }

   public void SetEnvironment(string environmentName)
   {
      System.Environment.SetEnvironmentVariable(EnvironmentVariableName, environmentName);
      this._environment.EnvironmentName = environmentName;
   }

   public bool IsDevelopEnvironment => string.Equals(Environments.Development, this.EnvironmentName, StringComparison.OrdinalIgnoreCase) || this._environment.IsDevelopment();

   public bool IsStagingEnvironment => string.Equals(Environments.Staging, this.EnvironmentName, StringComparison.OrdinalIgnoreCase) || this._environment.IsStaging();

   public bool IsProductionEnvironment => string.Equals(Environments.Production, this.EnvironmentName, StringComparison.OrdinalIgnoreCase) || this._environment.IsProduction();

   public bool IsAzureEnvironment => string.Equals(Environments.Azure, this.EnvironmentName, StringComparison.OrdinalIgnoreCase);

   public bool IsDockerEnvironment => string.Equals(Environments.Docker, this.EnvironmentName, StringComparison.OrdinalIgnoreCase);

   public bool IsUnitTestingEnvironment => string.Equals(Environments.UnitTesting, this.EnvironmentName, StringComparison.OrdinalIgnoreCase);

   public bool IsFunctionalTestingEnvironment => string.Equals(Environments.FunctionalTesting, this.EnvironmentName, StringComparison.OrdinalIgnoreCase);

   public bool IsEnvironment(string environmentName) => string.Equals(environmentName, this.EnvironmentName, StringComparison.OrdinalIgnoreCase) || this._environment.IsEnvironment(environmentName);

   public bool IsWindowsService => WindowsServiceHelpers.IsWindowsService();

   public StringBuilder ListEnvironmentVariables()
   {
      var variables = System.Environment.GetEnvironmentVariables();

      var sb = new StringBuilder();
      foreach (DictionaryEntry variable in variables)
      {
         sb.AppendLine($"{variable.Key} - {variable.Value}");
      }

      return sb;
   }
}
