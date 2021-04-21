using System;
using System.Collections.Generic;

namespace Snijderman.Common.SqlLocalDb
{
   public interface ISqlLocalDbInstance
   {
      void Start();

      void Shutdown();

      IReadOnlyList<string> GetInstanceNames { get; }
   }
}
