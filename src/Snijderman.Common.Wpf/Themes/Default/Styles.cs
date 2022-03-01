using System.Windows;

namespace Snijderman.Common.Wpf.Themes.Default;

public static class Styles
{
   // Buttons
   public static ComponentResourceKey BaseButtonKey => new(typeof(Styles), "BaseButton");
   public static ComponentResourceKey SystemButtonBaseKey => new(typeof(Styles), "SystemButtonBase");
   public static ComponentResourceKey AlertButtonKey => new(typeof(Styles), "AlertButton");
   public static ComponentResourceKey SystemButtonKey => new(typeof(Styles), "SystemButton");
   public static ComponentResourceKey SystemCloseButtonKey => new(typeof(Styles), "SystemCloseButton");
   public static ComponentResourceKey SystemButtonLinkKey => new(typeof(Styles), "SystemButtonLink");
   public static ComponentResourceKey WindowButtonKey => new(typeof(Styles), "WindowButton");
   public static ComponentResourceKey WindowCloseButtonKey => new(typeof(Styles), "WindowCloseButton");
}
