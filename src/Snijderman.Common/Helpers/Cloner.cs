using System.Text.Json;

namespace Snijderman.Common.Helpers
{
   public static class Cloner
   {
      public static T Clone<T>(this T source)
      {
         if (source == null)
         {
            return default;
         }

         return JsonSerializer.Deserialize<T>(JsonSerializer.Serialize(source));
      }
   }
}
