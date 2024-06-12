using System;

namespace Snijderman.Common.Http;

[Serializable]
public class ApiException : Exception
{
   public ApiException()
   {
   }

   public ApiException(string message) : base(message)
   {
   }

   public ApiException(string message, Exception innerException) : base(message, innerException)
   {
   }

   public int StatusCode { get; set; }

   public string Content { get; set; }
}
