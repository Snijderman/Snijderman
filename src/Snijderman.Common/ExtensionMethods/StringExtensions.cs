using System;
using System.ComponentModel;
using System.Linq;

namespace Snijderman.Common.ExtensionMethods;

public static class StringExtensions
{
   public static int? ToNullableInt(this string s)
   {
      if (int.TryParse(s, out var i))
      {
         return i;
      }

      return null;
   }

   public static string GetNullForEmptyString(this string s) => string.IsNullOrEmpty(s) ? null : s;

   public static T? ToEnum<T>(this string value) where T : struct
   {
      if (string.IsNullOrWhiteSpace(value))
      {
         return null;
      }

      if (Enum.TryParse(value, true, out T result))
      {
         return result;
      }

      return null;
   }

   public static bool CaseInsensitiveContains(this string source, string toCheck) => source != null && toCheck != null && source.Contains(toCheck, StringComparison.OrdinalIgnoreCase);

   public static bool CaseInsensitiveEquals(this string source, string toCheck) => string.Equals(source, toCheck, StringComparison.OrdinalIgnoreCase);

   public static bool ContainsOnly(this string value, char charachterToCheck)
   {
      if (string.IsNullOrWhiteSpace(value))
      {
         return false;
      }

      return value.All(x => x == charachterToCheck);
   }

   public static string Left(this string value, int maxLength)
   {
      if (string.IsNullOrEmpty(value))
      {
         return value;
      }

      var length = Math.Abs(maxLength);

      if (value.Length <= length)
      {
         return value;
      }

      return value.Substring(0, length);
   }

   public static T? ToNullable<T>(this string value) where T : struct
   {
      var result = new T?();
      try
      {
         if (!string.IsNullOrWhiteSpace(value))
         {
            var conv = TypeDescriptor.GetConverter(typeof(T));
            result = (T)conv.ConvertFrom(value);
         }
      }
      catch
      {
         // ignore error, return default value for T
      }

      return result;
   }

   public static bool ToBool(this string value)
   {
      if (string.IsNullOrWhiteSpace(value))
      {
         return false;
      }

      if (string.Equals("true", value, StringComparison.OrdinalIgnoreCase) || value == "1")
      {
         return true;
      }

      return false;
   }
}
