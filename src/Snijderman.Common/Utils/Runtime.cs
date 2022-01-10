using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using Snijderman.Common.Environment;

namespace Snijderman.Common.Utils
{
   public static class Runtime
   {
      private static readonly Assembly _entryAssembly = Assembly.GetEntryAssembly();
      private static readonly Assembly _executingAssembly = Assembly.GetExecutingAssembly();

      public static Guid InstanceId { get; } = Guid.NewGuid();

      public static string FrameworkDescription => RuntimeInformation.FrameworkDescription;

      public static string TargetFramework => _entryAssembly.GetCustomAttribute<TargetFrameworkAttribute>()?.FrameworkName;

      public static string SystemVersion => RuntimeEnvironment.GetSystemVersion();

      public static string OSArchitecture => RuntimeInformation.OSArchitecture.ToString();

      public static string OSDescription => RuntimeInformation.OSDescription;

      public static string ProcessArchitecture => RuntimeInformation.ProcessArchitecture.ToString();

      public static string GetExecutingAssemblyVersion() => _executingAssembly.GetName().Version.ToString();

      public static string GetExecutingAssemblyPath() => Path.GetDirectoryName(_executingAssembly.Location);

      public static string GetEntryAssemblyVersion() => _entryAssembly.GetName().Version.ToString();

      public static string GetEntryAssemblyPath() => Path.GetDirectoryName(_entryAssembly.Location);

      public static string GetHostingProcess() => Process.GetCurrentProcess().ProcessName;

      public static string GetFileVersion<T>() => FileVersionInfo.GetVersionInfo(Assembly.GetAssembly(typeof(T)).Location)?.FileVersion;

      public static dynamic GetRunningVersionInfo(IEnvironmentService environmentService) => new
      {
         Version = $"{GetEntryAssemblyVersion()} ({ProcessArchitecture})",
         Environment = $"{environmentService?.EnvironmentName}",
         OSPlatform = $"{OSDescription} ({OSArchitecture})",
         DotnetVersion = $"{TargetFramework} ({FrameworkDescription})",
         HostingProcess = GetHostingProcess()
      };
   }
}
