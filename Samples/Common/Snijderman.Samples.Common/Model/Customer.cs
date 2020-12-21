using System.Collections.Generic;

namespace Snijderman.Samples.Common.Model
{
   public class Customer
   {
      public string CompanyID { get; set; }

      public string CompanyName { get; set; }

      public string ContactName { get; set; }

      public string ContactTitle { get; set; }

      public string Address { get; set; }

      public string City { get; set; }

      public string PostalCode { get; set; }

      public string Country { get; set; }

      public string Phone { get; set; }

      public string Fax { get; set; }

      public ICollection<Order> Orders { get; set; } = new List<Order>();
   }
}
