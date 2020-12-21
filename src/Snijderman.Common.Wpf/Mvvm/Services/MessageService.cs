using System;
using System.Collections.Generic;
using System.Linq;

namespace Snijderman.Common.Wpf.Mvvm.Services
{
   public class MessageService : IMessageService
   {
      private readonly object _sync = new();

      private readonly List<Subscriber> _subscribers = new();

      public void Subscribe<TSender>(object target, Action<TSender, string, object> action) where TSender : class
      {
         this.Subscribe<TSender, object>(target, action);
      }
      public void Subscribe<TSender, TArgs>(object target, Action<TSender, string, TArgs> action) where TSender : class
      {
         if (target == null)
            throw new ArgumentNullException(nameof(target));
         if (action == null)
            throw new ArgumentNullException(nameof(action));

         lock (this._sync)
         {
            var subscriber = this._subscribers.FirstOrDefault(r => r.Target == target);
            if (subscriber == null)
            {
               subscriber = new Subscriber(target);
               this._subscribers.Add(subscriber);
            }
            subscriber.AddSubscription(action);
         }
      }

      public void Unsubscribe<TSender>(object target) where TSender : class
      {
         if (target == null)
            throw new ArgumentNullException(nameof(target));

         lock (this._sync)
         {
            var subscriber = this._subscribers.FirstOrDefault(r => r.Target == target);
            if (subscriber != null)
            {
               subscriber.RemoveSubscription<TSender>();
               if (subscriber.IsEmpty)
               {
                  this._subscribers.Remove(subscriber);
               }
            }
         }
      }
      public void Unsubscribe<TSender, TArgs>(object target) where TSender : class
      {
         if (target == null)
            throw new ArgumentNullException(nameof(target));

         lock (this._sync)
         {
            var subscriber = this._subscribers.FirstOrDefault(r => r.Target == target);
            if (subscriber != null)
            {
               subscriber.RemoveSubscription<TSender, TArgs>();
               if (subscriber.IsEmpty)
               {
                  this._subscribers.Remove(subscriber);
               }
            }
         }
      }
      public void Unsubscribe(object target)
      {
         if (target == null)
            throw new ArgumentNullException(nameof(target));

         lock (this._sync)
         {
            var subscriber = this._subscribers.FirstOrDefault(r => r.Target == target);
            if (subscriber != null)
            {
               this._subscribers.Remove(subscriber);
            }
         }
      }

      public void Send<TSender, TArgs>(TSender sender, string message, TArgs args) where TSender : class
      {
         if (sender == null)
            throw new ArgumentNullException(nameof(sender));

         foreach (var subscriber in this.GetSubscribersSnapshot())
         {
            // Avoid sending message to self
            if (subscriber.Target != sender)
            {
               subscriber.TryInvoke(sender, message, args);
            }
         }
      }

      private Subscriber[] GetSubscribersSnapshot()
      {
         lock (this._sync)
         {
            return this._subscribers.ToArray();
         }
      }

      class Subscriber
      {
         private readonly WeakReference _reference = null;

         private readonly Dictionary<Type, Subscriptions> _subscriptions;

         public Subscriber(object target)
         {
            this._reference = new WeakReference(target);
            this._subscriptions = new Dictionary<Type, Subscriptions>();
         }

         public object Target => this._reference.Target;

         public bool IsEmpty => this._subscriptions.Count == 0;

         public void AddSubscription<TSender, TArgs>(Action<TSender, string, TArgs> action)
         {
            if (!this._subscriptions.TryGetValue(typeof(TSender), out var subscriptions))
            {
               subscriptions = new Subscriptions();
               this._subscriptions.Add(typeof(TSender), subscriptions);
            }
            subscriptions.AddSubscription(action);
         }

         public void RemoveSubscription<TSender>()
         {
            this._subscriptions.Remove(typeof(TSender));
         }
         public void RemoveSubscription<TSender, TArgs>()
         {
            if (this._subscriptions.TryGetValue(typeof(TSender), out var subscriptions))
            {
               subscriptions.RemoveSubscription<TArgs>();
               if (subscriptions.IsEmpty)
               {
                  this._subscriptions.Remove(typeof(TSender));
               }
            }
         }

         public void TryInvoke<TArgs>(object sender, string message, TArgs args)
         {
            var target = this._reference.Target;
            if (this._reference.IsAlive)
            {
               var senderType = sender.GetType();
               foreach (var keyValue in this._subscriptions.Where(r => r.Key.IsAssignableFrom(senderType)))
               {
                  var subscriptions = keyValue.Value;
                  subscriptions.TryInvoke(sender, message, args);
               }
            }
         }
      }

      class Subscriptions
      {
         private readonly Dictionary<Type, Delegate> _subscriptions = null;

         public Subscriptions()
         {
            this._subscriptions = new Dictionary<Type, Delegate>();
         }

         public bool IsEmpty => this._subscriptions.Count == 0;

         public void AddSubscription<TSender, TArgs>(Action<TSender, string, TArgs> action)
         {
            this._subscriptions.Add(typeof(TArgs), action);
         }

         public void RemoveSubscription<TArgs>()
         {
            this._subscriptions.Remove(typeof(TArgs));
         }

         public void TryInvoke<TArgs>(object sender, string message, TArgs args)
         {
            var argsType = typeof(TArgs);
            foreach (var keyValue in this._subscriptions.Where(r => r.Key.IsAssignableFrom(argsType)))
            {
               var action = keyValue.Value;
               action?.DynamicInvoke(sender, message, args);
            }
         }
      }
   }
}
