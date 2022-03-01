using System;
using System.Collections.Generic;

namespace Snijderman.Samples.Common.Model;

public class Order
{
   public long OrderID { get; set; }

   public DateTime OrderDate { get; set; }

   public DateTime RequiredDate { get; set; }

   public DateTime ShippedDate { get; set; }

   public string ShipperName { get; set; }

   public string ShipperPhone { get; set; }

   public double Freight { get; set; }

   public string Company { get; set; }

   public string ShipTo { get; set; }

   public double OrderTotal { get; set; }

   public string Status { get; set; }

   public char Symbol => (char)this.SymbolCode;

   public int SymbolCode { get; set; }

   public ICollection<OrderDetail> Details { get; set; } = new List<OrderDetail>();

   public override string ToString() => $"{this.Company} {this.Status}";

   public string ShortDescription => $"Order ID: {this.OrderID}";
}
