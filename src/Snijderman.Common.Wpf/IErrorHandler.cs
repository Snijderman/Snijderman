using System;

namespace Snijderman.Common.Wpf
{
   public interface IErrorHandler
   {
      void HandleError(Exception exc);
   }
}
