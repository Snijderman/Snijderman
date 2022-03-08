using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Threading;

namespace Snijderman.Common.Wpf.Extensions;

public static class DispatcherObjectExtensions
{
   public static void InvokeIfRequired(this DispatcherObject control, Action methodcall, DispatcherPriority priorityForCall = DispatcherPriority.Normal)
   {
      if (control == null)
      {
         throw new ArgumentNullException(nameof(control));
      }

      if (methodcall == null)
      {
         throw new ArgumentNullException(nameof(methodcall));
      }

      //see if we need to Invoke call to Dispatcher thread
      if (control.Dispatcher != null && control.Dispatcher.Thread != Thread.CurrentThread)
      {
         control.Dispatcher.Invoke(priorityForCall, methodcall);
      }
      else
      {
         methodcall();
      }
   }

   public static T InvokeIfRequired<T>(this DispatcherObject control, Func<T> methodcall, DispatcherPriority priorityForCall = DispatcherPriority.Normal)
   {
      if (control == null)
      {
         throw new ArgumentNullException(nameof(control));
      }

      if (methodcall == null)
      {
         throw new ArgumentNullException(nameof(methodcall));
      }


      //see if we need to Invoke call to Dispatcher thread
      if (control.Dispatcher != null && control.Dispatcher.Thread != Thread.CurrentThread)
      {
         return control.Dispatcher.Invoke(methodcall, priorityForCall);
      }
      else
      {
         return methodcall();
      }
   }

   public static T InvokeIfRequired<T>(this Dispatcher dispatcher, Func<T> methodcall, DispatcherPriority priorityForCall = DispatcherPriority.Normal)
   {
      if (dispatcher == null)
      {
         throw new ArgumentNullException(nameof(dispatcher));
      }

      if (methodcall == null)
      {
         throw new ArgumentNullException(nameof(methodcall));
      }

      if (!dispatcher.CheckAccess() || dispatcher.Thread != Thread.CurrentThread)
      {
         return dispatcher.Invoke(methodcall, priorityForCall);
      }

      return methodcall();
   }

   // https://medium.com/@kevingosse/switching-back-to-the-ui-thread-in-wpf-uwp-in-modern-c-5dc1cc8efa5e
   public static SwitchToUiAwaitable SwitchToUi(this Dispatcher dispatcher) => new(dispatcher);

   public struct SwitchToUiAwaitable : INotifyCompletion
   {
      private readonly Dispatcher _dispatcher;

      public SwitchToUiAwaitable(Dispatcher dispatcher)
      {
         this._dispatcher = dispatcher;
      }

      public SwitchToUiAwaitable GetAwaiter() => this;

      public void GetResult()
      {
      }

      public bool IsCompleted => this._dispatcher.CheckAccess();

      public void OnCompleted(Action continuation) => this._dispatcher.BeginInvoke(continuation);
   }
}
