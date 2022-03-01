using System;
using System.Globalization;

namespace Snijderman.Common.ExtensionMethods;

public static class TypeExtensions
{
   public static T ChangeType<T>(this object value, CultureInfo cultureInfo)
   {
      if (value == null)
      {
         return default;
      }

      var toType = GetToType<T>();
      if (toType.Equals(value.GetType()))
      {
         return (T)value;
      }

      if (value is string s)
      {
         if (string.IsNullOrEmpty(s))
         {
            return ChangeType<T>(null, cultureInfo);
         }
         if (toType == typeof(Guid))
         {
            return ChangeType<T>(new Guid(s), cultureInfo);
         }
      }
      else if (toType.Equals(typeof(string)))
      {
         return ChangeType<T>(Convert.ToString(value, cultureInfo), cultureInfo);
      }

      var canConvert = CanTypeBeConverted(toType);
      if (canConvert)
      {
         return (T)Convert.ChangeType(value, toType, cultureInfo);
      }

      return (T)value;
   }

   private static Type GetToType<T>()
   {
      var toType = typeof(T);
      if (toType.IsGenericType && toType.GetGenericTypeDefinition() == typeof(Nullable<>))
      {
         toType = Nullable.GetUnderlyingType(toType);
      }

      return toType;
   }

   private static bool CanTypeBeConverted(Type type) => type is IConvertible || (type.IsValueType && !type.IsEnum);

   public static T ChangeType<T>(this object value) => ChangeType<T>(value, CultureInfo.InvariantCulture);

   public static bool IsDefault<T>(this T value) where T : struct
   {
      return value.Equals(default(T));
   }

   public static bool ImplementsInterface(this Type inputType, Type interfaceType)
   {
      if (inputType == default)
      {
         return false;
      }

      var interfaces = inputType.GetInterfaces();
      for (var i = 0; i < interfaces.Length; i++)
      {
         if (interfaces[i] == interfaceType)
         {
            return true;
         }
      }
      return false;
   }
}
