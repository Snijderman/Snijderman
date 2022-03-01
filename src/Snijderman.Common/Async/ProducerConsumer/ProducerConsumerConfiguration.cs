namespace Snijderman.Common.Async.ProducerConsumer;

public class ProducerConsumerConfiguration
{
   private int _readerCount = 1;

   public bool CreateBounded { get; set; } = true;

   public int Capacity { get; set; } = 1;

   public int ReaderCount
   {
      get => this._readerCount;
      set => this._readerCount = value > 0 ? value : 1;
   }

   public bool AllowSynchronousContinuations { get; set; }
}
