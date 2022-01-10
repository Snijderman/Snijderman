using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Snijderman.Common.ExtensionMethods
{
   public static class CollectionExtensions
   {
      public static int ListCount<T>(this IEnumerable<T> collection)
      {
         if (collection == null)
         {
            return 0;
         }

         return collection.Count();
      }

      public static void ExecuteBatchSize<T>(this ICollection<T> collection, int batchSize, Action<List<T>> action)
      {
         if (collection == null)
         {
            throw new ArgumentNullException(nameof(collection));
         }

         if (action == null)
         {
            throw new ArgumentNullException(nameof(action));
         }

         var loopCount = (int)Math.Ceiling((double)collection.ListCount() / batchSize);
         for (var i = 0; i < loopCount; i++)
         {
            var skip = i * batchSize;
            var batchChunk = collection.Skip(skip).Take(batchSize).ToList();
            action(batchChunk);
         }
      }

      public static void ExecuteBatchSizeParallel<T>(this ICollection<T> collection, int batchSize, Action<List<T>> action)
      {
         if (collection == null)
         {
            throw new ArgumentNullException(nameof(collection));
         }

         if (action == null)
         {
            throw new ArgumentNullException(nameof(action));
         }

         if (collection.ListCount() == 0)
         {
            return;
         }

         var loopCount = (int)Math.Ceiling((double)collection.ListCount() / batchSize);
         Parallel.For(0, loopCount, i =>
         {
            var skip = i * batchSize;
            var batchChunk = collection.Skip(skip).Take(batchSize).ToList();
            action(batchChunk);
         });
      }

      public static void AddRange<T>(this ICollection<T> target, IEnumerable<T> source)
      {
         if (target == null)
         {
            throw new ArgumentNullException(nameof(target));
         }

         if (source == null)
         {
            throw new ArgumentNullException(nameof(source));
         }

         foreach (var element in source)
         {
            target.Add(element);
         }
      }

      public static void AddKeyValuePair<TKey, TValue>(this IDictionary<TKey, TValue> target, KeyValuePair<TKey, TValue> item)
      {
         if (target == null)
         {
            throw new ArgumentNullException(nameof(target));
         }

         if (Equals(item.Key, default(TKey)))
         {
            return;
         }

         target.Add(item.Key, item.Value);
      }
   }
}
