using System.Collections.Generic;
using System.Threading.Tasks;
using Snijderman.Samples.Common.Model;

namespace Snijderman.Samples.Common.Services;

public interface IOrderDetailsService
{
   public Task<IEnumerable<OrderDetail>> GetOrderDetails(long orderId);
}
