namespace Snijderman.Common.Environment;

public static class Environments
{
   public static readonly string Development = Microsoft.Extensions.Hosting.Environments.Development;
   public static readonly string Staging = Microsoft.Extensions.Hosting.Environments.Staging;
   public static readonly string Production = Microsoft.Extensions.Hosting.Environments.Production;
   public static readonly string Azure = "Azure";
   public static readonly string Docker = "Docker";
   public static readonly string UnitTesting = "UnitTesting";
   public static readonly string FunctionalTesting = "FunctionalTesting";
}
