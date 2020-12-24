using System.Collections.Generic;
using System.Threading.Tasks;
using Snijderman.Samples.Common.Model;

namespace Snijderman.Samples.Common.Services
{
   public interface ISampleDataService
   {
      public Task<IEnumerable<Customer>> GetCustomersAsync();

      public Task<IEnumerable<Order>> GetOrdersAsync(string companyId);

      public Task<Order> GetOrderAsync(string companyId, long orderId);

      public Task<IEnumerable<OrderDetail>> GetOrderDetailsAsync(long orderId);
   }
}
