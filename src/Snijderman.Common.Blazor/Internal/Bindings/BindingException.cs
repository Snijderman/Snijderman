using System;

namespace Snijderman.Common.Blazor.Internal.Bindings;

public class BindingException : Exception
{
   public BindingException() { }

   public BindingException(string message) : base(message) { }

   public BindingException(string message, Exception innerException) : base(message, innerException) { }
}
