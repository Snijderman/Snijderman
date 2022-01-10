using System.Threading;
using System.Threading.Tasks;

namespace Snijderman.Common.Async.ProducerConsumer
{
   public interface IProducerConsumerChannelBase<T>
   {
      /// <summary>
      /// This method wil produce the content that can be picked up by the worker tasks
      /// </summary>
      /// <param name="contentToProduce">Content that should be produced</param>
      /// <param name="cancellationToken"></param>
      /// <returns></returns>
      Task ProduceAsync(T contentToProduce, CancellationToken cancellationToken = default);

      /// <summary>
      /// Complete the channel, so no new items can be produced and wait for the consumers to be done
      /// The channel is closed when all is done.
      /// </summary>
      /// <returns></returns>
      Task CompleteAsync();

      /// <summary>
      /// Get the number of consumers that is currently busy processing content
      /// </summary>
      int ActiveConsumerCount { get; }

      /// <summary>
      /// Indication if all data that is produced is picked up and processed by the consumer(s)
      /// </summary>
      bool AllProducedItemsConsumed { get; }

      /// <summary>
      /// Get the number of unprocessed queued items
      /// </summary>
      int ItemsInQueue { get; }
   }
}
