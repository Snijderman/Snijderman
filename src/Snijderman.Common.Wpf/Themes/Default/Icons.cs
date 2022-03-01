using System.Windows;

namespace Snijderman.Common.Wpf.Themes.Default;

public static class Icons
{
   public static ComponentResourceKey ErrorKey => new(typeof(Icons), "Error");

   public static ComponentResourceKey WindowMinimizeKey => new(typeof(Icons), "WindowMinimize");

   public static ComponentResourceKey WindowMaximizeKey => new(typeof(Icons), "WindowMaximize");

   public static ComponentResourceKey WindowRestoreKey => new(typeof(Icons), "WindowRestore");

   public static ComponentResourceKey WindowCloseKey => new(typeof(Icons), "WindowClose");
}
