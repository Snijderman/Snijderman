using System.Collections.Generic;
using System.Threading.Tasks;
using Snijderman.Samples.Common.Model;

namespace Snijderman.Samples.Common.Services;

public class CustomerService : ICustomerService
{
   private readonly ISampleDataService _sampleDataService;

   public CustomerService(ISampleDataService sampleDataService)
   {
      this._sampleDataService = sampleDataService;
   }
   public Task<IEnumerable<Customer>> GetCustomersAsync() => this._sampleDataService.GetCustomersAsync();
}
