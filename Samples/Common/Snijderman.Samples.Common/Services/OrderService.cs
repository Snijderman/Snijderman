using System.Collections.Generic;
using System.Threading.Tasks;
using Snijderman.Samples.Common.Model;

namespace Snijderman.Samples.Common.Services;

public class OrderService : IOrderService
{
   private readonly ISampleDataService _sampleDataService;

   public OrderService(ISampleDataService sampleDataService)
   {
      this._sampleDataService = sampleDataService;
   }

   public Task<IEnumerable<Order>> GetOrdersAsync(string companyId) => this._sampleDataService.GetOrdersAsync(companyId);

   public Task<Order> GetOrderAsync(string companyId, long orderId) => this._sampleDataService.GetOrderAsync(companyId, orderId);
}
