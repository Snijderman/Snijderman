using System.Collections.Generic;
using System.Threading.Tasks;
using Snijderman.Samples.Common.Model;

namespace Snijderman.Samples.Common.Services
{
   public class OrderDetailsService : IOrderDetailsService
   {
      private readonly ISampleDataService _sampleDataService;

      public OrderDetailsService(ISampleDataService sampleDataService)
      {
         this._sampleDataService = sampleDataService;
      }

      public Task<IEnumerable<OrderDetail>> GetOrderDetails(long orderId) => this._sampleDataService.GetOrderDetails(orderId);
   }
}
