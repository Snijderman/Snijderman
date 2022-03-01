using System.Windows;

namespace Snijderman.Common.Wpf.Themes.Default;

public static class Dimensions
{
   public static ComponentResourceKey CornerRadius => new(typeof(Dimensions), "CornerRadius");

   public static ComponentResourceKey BorderThickness => new(typeof(Dimensions), "BorderThickness");

   public static ComponentResourceKey HorizontalSpace => new(typeof(Dimensions), "HorizontalSpace");

   public static ComponentResourceKey VerticalSpace => new(typeof(Dimensions), "VerticalSpace");

   public static ComponentResourceKey CursorSpotlightRelativeSize => new(typeof(double), "CursorSpotlightRelativeSize");
}
