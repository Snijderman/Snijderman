using System;

namespace Snijderman.Common.Helpers;

public static class UriExtensions
{
   public static string BuildEndpointAddress(this Uri uri, string path)
   {
      if (uri == default)
      {
         return path;
      }

      if (string.IsNullOrEmpty(path))
      {
         return uri.AbsoluteUri;
      }

      if (path.StartsWith("/", StringComparison.OrdinalIgnoreCase))
      {
         path = path[1..];
      }

      return $"{uri.AbsoluteUri}{path}";
   }
}
