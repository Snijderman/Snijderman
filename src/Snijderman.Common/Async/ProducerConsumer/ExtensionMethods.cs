using System.Text;
using Snijderman.Common.ExtensionMethods;

namespace Snijderman.Common.Async.ProducerConsumer;

public static class ExtensionMethods
{
   public static string ExtractProducerConsumerExceptionMessages(this ProducerConsumerException exception)
   {
      if (exception == default)
      {
         return default;
      }

      const string errorSeperator = "================================================================================================";
      var sb = new StringBuilder();
      sb.AppendLine(exception.ToString());
      for (var i = 0; i < exception.InnerExceptions.ListCount(); i++)
      {
         sb.AppendLine(errorSeperator);
         sb.AppendLine(exception.InnerExceptions[i].Message);
         if (exception.InnerExceptions[i] is ProducerConsumerException producerConsumerException)
         {
            sb.AppendLine(errorSeperator);
            sb.AppendLine(producerConsumerException.ExtractProducerConsumerExceptionMessages());
         }
         else
         {
            sb.AppendLine(errorSeperator);
            sb.AppendLine(exception.InnerExceptions[i].ToString());
         }
      }

      return sb.ToString();
   }
}
