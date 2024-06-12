using System.Collections.Generic;
using System.Threading.Tasks;

namespace Snijderman.Common.ExtensionMethods;

public static class AsyncEnumerableExtensions
{
   public static async Task<IEnumerable<T>> GetAsEnumerable<T>(this IAsyncEnumerable<T> asyncEnumerable)
   {
      var result = new List<T>();
      await foreach (var item in asyncEnumerable)
      {
         result.Add(item);
      }

      return result;
   }
}
