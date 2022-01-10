using System;
using System.ComponentModel;

namespace Snijderman.Common.Utils
{
   public static class Convert
   {
      public static int KilobytesToMegabytes(int kilobytes)
      {
         return kilobytes / 1024;
      }

      public static int KilobytesFromMegabytes(int megabytes)
      {
         const int bytes = 1024;
         if (megabytes > int.MaxValue / bytes)
         {
            throw new InvalidOperationException("Megabytes is too large to convert to kilobytes");
         }

         return System.Convert.ToInt32(megabytes * bytes);
      }

      public static T Parse<T>(object value)
      {
         if (value == null)
         {
            throw new ArgumentNullException(nameof(value));
         }

         // or ConvertFromInvariantString if you are doing serialization
         return (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(value.ToString());
      }
   }
}
