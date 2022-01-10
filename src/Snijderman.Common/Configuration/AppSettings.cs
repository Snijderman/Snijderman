using System.Collections.Generic;

namespace Snijderman.Common.Configuration
{
   public class AppSettings
   {
      public List<(string Key, string Value)> Settings { get; } = new List<(string Key, string Value)>();
   }
}
