using System;
using System.Threading.Tasks;
using Snijderman.Common.Utils;

namespace Snijderman.Common.ExtensionMethods;

public static class TaskExtensions
{
   public static async void Await(this Task task, Action completedCallback, Action<Exception> exceptionCallback)
   {
      if (task is null)
      {
         throw new ArgumentNullException(nameof(task));
      }

      try
      {
         await task.ConfigureAwait(false);
         completedCallback?.Invoke();
      }
      catch (Exception exc)
      {
         exceptionCallback?.Invoke(exc);
      }
   }

   public static async void Await<T>(this Task<T> task, Action<T> completedCallback, Action<Exception> exceptionCallback)
   {
      if (task is null)
      {
         throw new ArgumentNullException(nameof(task));
      }

      try
      {
         var ret = await task.ConfigureAwait(false);
         completedCallback?.Invoke(ret);
      }
      catch (Exception exc)
      {
         exceptionCallback?.Invoke(exc);
      }
   }

   public static T AwaitResult<T>(this Task<T> task)
   {
      if (task is null)
      {
         throw new ArgumentNullException(nameof(task));
      }

      return AsyncUtil.RunSync(() => task);
   }
}
