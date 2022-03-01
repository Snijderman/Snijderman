using System.Windows.Media;

namespace Snijderman.Common.Wpf.Helpers;

public static class ColorHelper
{
   public static Color GetColorFromHtmlRgbCode(string rgbCode)
   {
      return ConvertToMediaColor(System.Drawing.ColorTranslator.FromHtml(rgbCode));
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
