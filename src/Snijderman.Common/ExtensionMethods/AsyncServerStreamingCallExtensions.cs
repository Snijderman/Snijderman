using System.Collections.Generic;
using System.Threading.Tasks;
using Grpc.Core;

namespace Snijderman.Common.ExtensionMethods;

public static class AsyncServerStreamingCallExtensions
{
   public static Task<IEnumerable<T>> GetAsEnumerable<T>(this AsyncServerStreamingCall<T> serverStreamingCall)
   {
      return serverStreamingCall.ResponseStream.ReadAllAsync().GetAsEnumerable();
   }
}
