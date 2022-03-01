using System;
using System.Collections.Generic;
using System.Linq;

namespace Snijderman.Common.ExtensionMethods;

public static class EnumExtensions
{
   public static List<string> GetEnumNames<T>() where T : struct
   {
      if (!typeof(T).IsEnum)
      {
         throw new ArgumentException($"Type {typeof(T)} is not an enum");
      }

      return Enum.GetNames(typeof(T)).ToList();
   }

#pragma warning disable IDE0060 // Remove unused parameter
   public static List<string> GetEnumNames<T>(this T enumInstance) where T : struct
#pragma warning restore IDE0060 // Remove unused parameter
   {
      return GetEnumNames<T>();
   }
}
