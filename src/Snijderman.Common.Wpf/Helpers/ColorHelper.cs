using System.Drawing;
using Color = System.Windows.Media.Color;
using ColorConverter = System.Windows.Media.ColorConverter;

namespace Snijderman.Common.Wpf.Helpers;

public static class ColorHelper
{
   public static Color GetColorFromHtmlRgbCode(string rgbCode)
   {
      return ConvertToMediaColor(ColorTranslator.FromHtml(rgbCode));
   }

   public static Color GetColorFromArgbHexCode(string argbHexCode)
   {
      return (Color?)ColorConverter.ConvertFromString(argbHexCode) ?? default;
   }

   private static Color ConvertToMediaColor(System.Drawing.Color color)
   {
      return Color.FromArgb(color.A, color.R, color.G, color.B);
   }
}
