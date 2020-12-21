using System.Collections.Generic;
using System.Threading.Tasks;
using Snijderman.Samples.Common.Model;

namespace Snijderman.Samples.Common.Services
{
   public interface ISampleDataService
   {
      public Task<IEnumerable<Customer>> GetCustomers();

      public Task<IEnumerable<Order>> GetOrders(string companyId);

      public Task<IEnumerable<OrderDetail>> GetOrderDetails(long orderId);
   }
}
