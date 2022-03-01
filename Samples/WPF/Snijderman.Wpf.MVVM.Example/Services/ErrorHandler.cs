using System;
using Snijderman.Common.Wpf;

namespace Snijderman.Wpf.MVVM.Example.Services;

public class ErrorHandler : IErrorHandler
{
   public void HandleError(Exception exc)
   {
      // peform some logging
      throw exc;
   }

}
