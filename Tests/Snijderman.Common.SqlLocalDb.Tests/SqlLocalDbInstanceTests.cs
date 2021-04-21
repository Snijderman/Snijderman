using System;
using Xunit;

namespace Snijderman.Common.SqlLocalDb.Tests
{
   public class SqlLocalDbInstanceTests
   {
      [Fact]
      public void Create_A_New_Instance_And_Shutdown()
      {
         var instanceId = Guid.NewGuid().ToString();
         using var instance = new SqlLocalDbInstance(instanceId);
         instance.Start();
         Assert.Contains(instance.GetInstanceNames, x => string.Equals(x, instanceId, StringComparison.OrdinalIgnoreCase));
         instance.Shutdown();
         Assert.DoesNotContain(instance.GetInstanceNames, x => string.Equals(x, instanceId, StringComparison.OrdinalIgnoreCase));
      }

      [Fact]
      public void Create_A_New_Instance_Start_Multiple_Times_And_Do_Not_Call_Shutdown()
      {
         var instanceId = Guid.NewGuid().ToString();
         using var instance = new SqlLocalDbInstance(instanceId);
         instance.Start();
         instance.Start();
         Assert.Contains(instance.GetInstanceNames, x => string.Equals(x, instanceId, StringComparison.OrdinalIgnoreCase));
      }

      [Fact]
      public void Create_A_New_Instance_Do_Not_Start_And_Call_Shutdown()
      {
         var instanceId = Guid.NewGuid().ToString();
         using var instance = new SqlLocalDbInstance(instanceId);
         Assert.DoesNotContain(instance.GetInstanceNames, x => string.Equals(x, instanceId, StringComparison.OrdinalIgnoreCase));
         instance.Shutdown();
         Assert.DoesNotContain(instance.GetInstanceNames, x => string.Equals(x, instanceId, StringComparison.OrdinalIgnoreCase));
      }
   }
}
