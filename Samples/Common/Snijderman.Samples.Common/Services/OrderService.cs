using System.Collections.Generic;
using System.Threading.Tasks;
using Snijderman.Samples.Common.Model;

namespace Snijderman.Samples.Common.Services
{
   public class OrderService : IOrderService
   {
      private readonly ISampleDataService _sampleDataService;

      public OrderService(ISampleDataService sampleDataService)
      {
         this._sampleDataService = sampleDataService;
      }

      public Task<IEnumerable<Order>> GetOrders(string companyId) => this._sampleDataService.GetOrders(companyId);
   }
}
