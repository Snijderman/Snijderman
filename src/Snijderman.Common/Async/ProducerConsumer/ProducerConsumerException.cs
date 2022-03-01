using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Snijderman.Common.Async.ProducerConsumer;

[Serializable]
public class ProducerConsumerException : Exception
{
   public ProducerConsumerException()
   {
   }

   public ProducerConsumerException(string message) : base(message)
   {
   }

   public ProducerConsumerException(string message, Exception innerException) : base(message, innerException)
   {
   }

   public ProducerConsumerException(string message, IList<Exception> innerExceptions) : base(message)
   {
      this.InnerExceptions = innerExceptions;
   }

   protected ProducerConsumerException(SerializationInfo info, StreamingContext context) : base(info, context)
   {
   }

   public IList<Exception> InnerExceptions { get; private set; } = new List<Exception>();
}
