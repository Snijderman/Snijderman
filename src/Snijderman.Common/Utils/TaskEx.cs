using System;
using System.Threading.Tasks;

namespace Snijderman.Common.Utils;

public static class TaskEx
{
   public static async Task<(T1, T2)> WhenAll<T1, T2>(Task<T1> task1, Task<T2> task2)
   {
      if (task1 == null)
      {
         throw new ArgumentNullException(nameof(task1));
      }

      if (task2 == null)
      {
         throw new ArgumentNullException(nameof(task2));
      }

      await Task.WhenAll(task1, task2).ConfigureAwait(false);
      return (task1.Result, task2.Result);
   }

   public static async Task<(T1, T2, T3)> WhenAll<T1, T2, T3>(Task<T1> task1, Task<T2> task2, Task<T3> task3)
   {
      if (task1 == null)
      {
         throw new ArgumentNullException(nameof(task1));
      }

      if (task2 == null)
      {
         throw new ArgumentNullException(nameof(task2));
      }

      if (task3 == null)
      {
         throw new ArgumentNullException(nameof(task3));
      }

      await Task.WhenAll(task1, task2, task3).ConfigureAwait(false);
      return (task1.Result, task2.Result, task3.Result);
   }

   public static async Task<(T1, T2, T3, T4)> WhenAll<T1, T2, T3, T4>(Task<T1> task1, Task<T2> task2, Task<T3> task3, Task<T4> task4)
   {
      if (task1 == null)
      {
         throw new ArgumentNullException(nameof(task1));
      }

      if (task2 == null)
      {
         throw new ArgumentNullException(nameof(task2));
      }

      if (task3 == null)
      {
         throw new ArgumentNullException(nameof(task3));
      }

      if (task4 == null)
      {
         throw new ArgumentNullException(nameof(task4));
      }

      await Task.WhenAll(task1, task2, task3, task4).ConfigureAwait(false);
      return (task1.Result, task2.Result, task3.Result, task4.Result);
   }

   public static async Task<(T1, T2, T3, T4, T5)> WhenAll<T1, T2, T3, T4, T5>(Task<T1> task1, Task<T2> task2, Task<T3> task3, Task<T4> task4, Task<T5> task5)
   {
      if (task1 == null)
      {
         throw new ArgumentNullException(nameof(task1));
      }

      if (task2 == null)
      {
         throw new ArgumentNullException(nameof(task2));
      }

      if (task3 == null)
      {
         throw new ArgumentNullException(nameof(task3));
      }

      if (task4 == null)
      {
         throw new ArgumentNullException(nameof(task4));
      }

      if (task5 == null)
      {
         throw new ArgumentNullException(nameof(task5));
      }

      await Task.WhenAll(task1, task2, task3, task4, task5).ConfigureAwait(false);
      return (task1.Result, task2.Result, task3.Result, task4.Result, task5.Result);
   }
}
