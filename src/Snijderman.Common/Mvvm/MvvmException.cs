using System;

namespace Snijderman.Common.Mvvm;

public class MvvmException : Exception
{
   public MvvmException()
   { }

   public MvvmException(string message) : base(message)
   { }
}
