using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Snijderman.Common.Async.ProducerConsumer
{
   public abstract class ProducerConsumerChannelBase<T> : IProducerConsumerChannelBase<T>
   {
      protected ILogger Logger { get; }
      private readonly ProducerConsumerConfiguration _configuration;
      private readonly Channel<T> _channel;
      private readonly Task[] _readerTasks;

      protected ProducerConsumerChannelBase(ProducerConsumerConfiguration configuration = default, ILoggerFactory loggerFactory = default)
      {
         this._configuration = configuration ?? new ProducerConsumerConfiguration();
         this.Logger = loggerFactory?.CreateLogger(this.GetType());
         this._channel = this.CreateChannel();
         this._readerTasks = this.CreateReaderTasks();
      }

      private Channel<T> CreateChannel()
      {
         return this._configuration.CreateBounded ? this.CreateBoundedChannel() : this.CreateUnboundedChannel();
      }

      private Channel<T> CreateBoundedChannel()
      {
         return Channel.CreateBounded<T>(this.CreateBoundedChannelOptions());
      }

      private BoundedChannelOptions CreateBoundedChannelOptions()
      {
         return new BoundedChannelOptions(this._configuration.Capacity)
         {
            SingleWriter = true,
            SingleReader = this._configuration.ReaderCount == 1
         };
      }

      private Channel<T> CreateUnboundedChannel()
      {
         return Channel.CreateUnbounded<T>(this.CreateUnboundedChannelOptions());
      }

      private UnboundedChannelOptions CreateUnboundedChannelOptions()
      {
         return new UnboundedChannelOptions
         {
            SingleWriter = true,
            SingleReader = this._configuration.ReaderCount == 1
         };
      }

      private Task[] CreateReaderTasks()
      {
         return Enumerable.Range(1, this._configuration.ReaderCount)
                          .Select(i => new Consumer(this._channel.Reader, this.InternalConsumeAsync, this.Logger).BeginConsumeAsync())
                          .ToArray();
      }

      private volatile int _itemsInQueue;
      public int ItemsInQueue => this._itemsInQueue;

      public virtual bool AllProducedItemsConsumed
      {
         get
         {
            this.CheckIfConsumerExceptionsOccured();
            return this._itemsInQueue == 0 && this._activeConsumerCount == 0;
         }
      }

      private volatile int _activeConsumerCount;
      public int ActiveConsumerCount => this._activeConsumerCount;

      private readonly ConcurrentBag<Exception> _consumerExceptions = new();
      private void CheckIfConsumerExceptionsOccured()
      {
         if (this._consumerExceptions.IsEmpty)
         {
            return;
         }

         throw new ProducerConsumerException("Exception(s) occured during processing of consumer", this._consumerExceptions.ToList());
      }

      private long _produceCalledCount;
      protected long ProduceCalledCount => this._produceCalledCount;

      public virtual async Task ProduceAsync(T contentToProduce, CancellationToken cancellationToken = default)
      {
         this.CheckIfConsumerExceptionsOccured();
         Interlocked.Increment(ref this._produceCalledCount);
         await this._channel.Writer.WriteAsync(contentToProduce, cancellationToken).ConfigureAwait(false);
         Interlocked.Increment(ref this._itemsInQueue);
      }

      private long _consumeCalledCount;
      protected long ConsumeCalledCount => this._consumeCalledCount;

      private async Task InternalConsumeAsync(T producedContent)
      {
         Interlocked.Increment(ref this._consumeCalledCount);
         if (!this._consumerExceptions.IsEmpty)
         {
            Interlocked.Decrement(ref this._itemsInQueue);
            return;
         }
         Interlocked.Increment(ref this._activeConsumerCount);
         Interlocked.Decrement(ref this._itemsInQueue);
         try
         {
            await this.ConsumeAsync(producedContent).ConfigureAwait(false);
         }
         catch (Exception exc)
         {
            this._consumerExceptions.Add(exc);
         }
         finally
         {
            Interlocked.Decrement(ref this._activeConsumerCount);
         }
      }

      protected abstract Task ConsumeAsync(T producedContent);

      public virtual async Task CompleteAsync()
      {
         this.CheckIfConsumerExceptionsOccured();
         // closing writer
         this._channel.Writer.Complete();
         // wait for readers to complete
         await this._channel.Reader.Completion.ConfigureAwait(false);
         // wait till all the reader tasks are completed
         await Task.WhenAll(this._readerTasks).ConfigureAwait(false);

         this.CheckIfConsumerExceptionsOccured();
      }

      private class Consumer
      {
         private readonly ChannelReader<T> _reader;
         private readonly Func<T, Task> _consumeAction;

         //private readonly Task _consumeTask;
         private readonly ILogger _logger;
         private readonly Guid _instanceId = Guid.NewGuid();

         internal Consumer(ChannelReader<T> reader, Func<T, Task> consumeAction, ILogger logger)
         {
            this._reader = reader;
            this._consumeAction = consumeAction;
            this._logger = logger;
         }

         internal async Task BeginConsumeAsync(CancellationToken cancellationToken = default)
         {
            this._logger?.LogDebug($"{this._instanceId} > starting");
            try
            {
#if NETSTANDARD2_1
               while (await this._reader.WaitToReadAsync(cancellationToken).ConfigureAwait(false))//if this returns false the channel is completed
               {
                  while (this._reader.TryRead(out var producedContent))
                  {
                     this._logger?.LogDebug($"{this._instanceId} > Received item, invoke consume action");
                     await this._consumeAction(producedContent).ConfigureAwait(false);
                     this._logger?.LogDebug($"{this._instanceId} > Processed item");
                  }
               }
#else
               await foreach (var producedContent in this._reader.ReadAllAsync(cancellationToken).ConfigureAwait(false))
               {
                  this._logger?.LogDebug($"{this._instanceId} > Received item, invoke consume action");
                  await this._consumeAction(producedContent).ConfigureAwait(false);
                  this._logger?.LogDebug($"{this._instanceId} > Processed item");
               }
#endif
            }
            catch (OperationCanceledException)
            {
               this._logger?.LogDebug($"{this._instanceId} > forced stop");
            }

            this._logger?.LogDebug($"{this._instanceId} > shutting down");
         }

      }
   }
}
