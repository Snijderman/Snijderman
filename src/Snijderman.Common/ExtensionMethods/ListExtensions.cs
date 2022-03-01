using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Snijderman.Common.ExtensionMethods;

public static class ListExtensions
{

   public static void TryAddRange<T>(this IList<T> list, IList<T> collection)
   {
      if (list == null || collection.ListCount() == 0)
      {
         return;
      }

      list.AddRange(collection);
   }

   public static void ExecuteFor<T>(this IList<T> list, Action<T> action)
   {
      if (list is null)
      {
         throw new ArgumentNullException(nameof(list));
      }

      if (action is null)
      {
         throw new ArgumentNullException(nameof(action));
      }

      for (var i = 0; i < list.ListCount(); i++)
      {
         action(list[i]);
      }
   }

   public static async Task ExecuteForAsync<T>(this IList<T> list, Func<T, Task> action)
   {
      if (list is null)
      {
         throw new ArgumentNullException(nameof(list));
      }

      if (action is null)
      {
         throw new ArgumentNullException(nameof(action));
      }

      for (var i = 0; i < list.ListCount(); i++)
      {
         await action(list[i]).ConfigureAwait(false);
      }
   }
}
