using System.Collections.Generic;
using System.Threading.Tasks;
using Snijderman.Samples.Common.Model;

namespace Snijderman.Samples.Common.Services;

public interface ICustomerService
{
   public Task<IEnumerable<Customer>> GetCustomers();
}
