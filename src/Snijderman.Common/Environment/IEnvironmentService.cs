using System.Text;

namespace Snijderman.Common.Environment
{
   public interface IEnvironmentService
   {
      public string EnvironmentName { get; }

      public void SetEnvironment(string environmentName);

      public bool IsDevelopEnvironment { get; }

      public bool IsAzureEnvironment { get; }

      public bool IsDockerEnvironment { get; }

      public bool IsUnitTestingEnvironment { get; }

      public bool IsFunctionalTestingEnvironment { get; }

      public bool IsWindowsService { get; }

      StringBuilder ListEnvironmentVariables();
   }
}
