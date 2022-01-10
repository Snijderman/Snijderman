using System;
using System.Globalization;

namespace Snijderman.Common.ExtensionMethods
{
   public static class DateTimeExtensions
   {
      public static DateTime GetDateWithoutTime(this DateTime value) => value.Date;

      public static DateTime GetDateWithoutTime(this DateTime? value)
      {
         if (!value.HasValue)
         {
            return DateTime.MinValue.Date;
         }

         return value.Value.GetDateWithoutTime();
      }

      public static DateTime? GetNullableDateWithoutTime(this DateTime? value) => value?.GetDateWithoutTime();

      public static DateTime Trim(this DateTime date, long roundTicks) => new(date.Ticks - date.Ticks % roundTicks);

      public static string FormattedDateTime(this DateTime? date)
      {
         if (date.HasValue)
         {
            return date.Value.FormattedDateTime();
         }

         return string.Empty;
      }

      public static string FormattedDateTime(this DateTime date) => date.ToString("dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);

      public static string FormattedDate(this DateTime? date)
      {
         if (date.HasValue)
         {
            return date.Value.FormattedDate();
         }

         return string.Empty;
      }

      public static string FormattedDate(this DateTime date)
      {
         return date.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
      }

      public static bool EqualsDate(this DateTime date, DateTime dateToCompareWith) => date.Date.Equals(dateToCompareWith.Date);
   }
}
