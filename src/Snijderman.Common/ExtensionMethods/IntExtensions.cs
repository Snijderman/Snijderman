namespace Snijderman.Common.ExtensionMethods
{
   public static class IntExtensions
   {
      public static string GetStringValue(this int value) => $"{value}";

      public static string GetStringValue(this int? value) => value.HasValue ? $"{value}" : null;

      public static T? ToItemEnum<T>(this int value) where T : struct => $"Item{value}".ToEnum<T>();

      public static T? ToItemEnum<T>(this int? value) where T : struct => value.HasValue ? $"Item{value}".ToEnum<T>() : null;
   }
}
