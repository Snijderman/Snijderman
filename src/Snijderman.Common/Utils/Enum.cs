namespace Snijderman.Common.Utils;

public static class Enum
{
   public static T ParseEnum<T>(string value)
   {
      return (T)System.Enum.Parse(typeof(T), value, true);
   }
}
