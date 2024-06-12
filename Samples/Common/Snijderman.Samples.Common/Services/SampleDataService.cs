using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Snijderman.Samples.Common.Model;

namespace Snijderman.Samples.Common.Services;

public class SampleDataService : ISampleDataService
{
   private static readonly IEnumerable<Customer> _allCompaniesOrdersAndOrderDetails = GetAllCompaniesOrdersAndOrderDetails();

   private static IEnumerable<Customer> GetAllCompaniesOrdersAndOrderDetails()
   {
      return new List<Customer>
      {
                new Customer
                {
                    CompanyId = "ALFKI",
                    CompanyName = "Company A",
                    ContactName = "Maria Anders",
                    ContactTitle = "Sales Representative",
                    Address = "Obere Str. 57",
                    City = "Berlin",
                    PostalCode = "12209",
                    Country = "Germany",
                    Phone = "030-0074321",
                    Fax = "030-0076545",
                    Orders = new List<Order>
                    {
                        new Order
                        {
                            OrderId = 10643, // Symbol Globe
                            OrderDate = new DateTime(1997, 8, 25),
                            RequiredDate = new DateTime(1997, 9, 22),
                            ShippedDate = new DateTime(1997, 9, 22),
                            ShipperName = "Speedy Express",
                            ShipperPhone = "(503) 555-9831",
                            Freight = 29.46,
                            Company = "Company A",
                            ShipTo = "Company A, Obere Str. 57, Berlin, 12209, Germany",
                            OrderTotal = 814.50,
                            Status = "Shipped",
                            SymbolCode = 57643,
                            Details = new List<OrderDetail>
                            {
                                new OrderDetail
                                {
                                    ProductId = 28,
                                    ProductName = "Rössle Sauerkraut",
                                    Quantity = 15,
                                    Discount = 0.25,
                                    QuantityPerUnit = "25 - 825 g cans",
                                    UnitPrice = 45.60,
                                    CategoryName = "Produce",
                                    CategoryDescription = "Dried fruit and bean curd",
                                    Total = 513.00
                                },
                                new OrderDetail
                                {
                                    ProductId = 39,
                                    ProductName = "Chartreuse verte",
                                    Quantity = 21,
                                    Discount = 0.25,
                                    QuantityPerUnit = "750 cc per bottle",
                                    UnitPrice = 18.0,
                                    CategoryName = "Beverages",
                                    CategoryDescription = "Soft drinks, coffees, teas, beers, and ales",
                                    Total = 283.50
                                },
                                new OrderDetail
                                {
                                    ProductId = 46,
                                    ProductName = "Spegesild",
                                    Quantity = 2,
                                    Discount = 0.25,
                                    QuantityPerUnit = "4 - 450 g glasses",
                                    UnitPrice = 12.0,
                                    CategoryName = "Seafood",
                                    CategoryDescription = "Seaweed and fish",
                                    Total = 18.00
                                }
                            }
                        },
                        new Order
                        {
                            OrderId = 1512, // Symbol Music
                            OrderDate = new DateTime(1998, 1, 15),
                            RequiredDate = new DateTime(1998, 2, 12),
                            ShippedDate = new DateTime(1998, 1, 21),
                            ShipperName = "Federal Shipping",
                            ShipperPhone = "(503) 555-9931",
                            Freight = 69.53,
                            Company = "Company A",
                            ShipTo = "Company A, Obere Str. 57, Berlin, 12209, Germany",
                            OrderTotal = 845.80,
                            Status = "Closed",
                            SymbolCode = 57737,
                            Details = new List<OrderDetail>
                            {
                                new OrderDetail
                                {
                                    ProductId = 59,
                                    ProductName = "Raclette Courdavault",
                                    Quantity = 15,
                                    Discount = 0,
                                    QuantityPerUnit = "5 kg pkg.",
                                    UnitPrice = 55.00,
                                    CategoryName = "Dairy Products",
                                    CategoryDescription = "Cheeses",
                                    Total = 825.00
                                },
                                new OrderDetail
                                {
                                    ProductId = 77,
                                    ProductName = "Original Frankfurter grüne Soße",
                                    Quantity = 2,
                                    Discount = 0.2,
                                    QuantityPerUnit = "12 boxes",
                                    UnitPrice = 13.0,
                                    CategoryName = "Condiments",
                                    CategoryDescription = "Sweet and savory sauces, relishes, spreads, and seasonings",
                                    Total = 20.80
                                }
                            }
                        },
                        new Order
                        {
                            OrderId = 10952, // Symbol Calendar
                            OrderDate = new DateTime(1998, 3, 16),
                            RequiredDate = new DateTime(1998, 4, 27),
                            ShippedDate = new DateTime(1998, 3, 24),
                            ShipperName = "Speedy Express",
                            ShipperPhone = "(503) 555-9831",
                            Freight = 40.42,
                            Company = "Company A",
                            ShipTo = "Company A, Obere Str. 57, Berlin, 12209, Germany",
                            OrderTotal = 471.20,
                            Status = "Closed",
                            SymbolCode = 57699,
                            Details = new List<OrderDetail>
                            {
                                new OrderDetail
                                {
                                    ProductId = 6,
                                    ProductName = "Grandma's Boysenberry Spread",
                                    Quantity = 16,
                                    Discount = 0.05,
                                    QuantityPerUnit = "12 - 8 oz jars",
                                    UnitPrice = 25.0,
                                    CategoryName = "Condiments",
                                    CategoryDescription = "Sweet and savory sauces, relishes, spreads, and seasonings",
                                    Total = 380.00
                                },
                                new OrderDetail
                                {
                                    ProductId = 28,
                                    ProductName = "Rössle Sauerkraut",
                                    Quantity = 2,
                                    Discount = 0,
                                    QuantityPerUnit = "25 - 825 g cans",
                                    UnitPrice = 45.60,
                                    CategoryName = "Produce",
                                    CategoryDescription = "Dried fruit and bean curd",
                                    Total = 91.20
                                }
                            }
                        }
                    }
                },
                new Customer
                {
                    CompanyId = "ANATR",
                    CompanyName = "Company F",
                    ContactName = "Ana Trujillo",
                    ContactTitle = "Owner",
                    Address = "Avda. de la Constitución 2222",
                    City = "México D.F.",
                    PostalCode = "05021",
                    Country = "Mexico",
                    Phone = "(5) 555-4729",
                    Fax = "(5) 555-3745",
                    Orders = new List<Order>
                    {
                        new Order
                        {
                            OrderId = 10625, // Symbol Camera
                            OrderDate = new DateTime(1997, 8, 8),
                            RequiredDate = new DateTime(1997, 9, 5),
                            ShippedDate = new DateTime(1997, 8, 14),
                            ShipperName = "Speedy Express",
                            ShipperPhone = "(503) 555-9831",
                            Freight = 43.90,
                            Company = "Company F",
                            ShipTo = "Company F, Avda. de la Constitución 2222, 05021, México D.F., Mexico",
                            OrderTotal = 469.75,
                            Status = "Shipped",
                            SymbolCode = 57620,
                            Details = new List<OrderDetail>
                            {
                                new OrderDetail
                                {
                                    ProductId = 14,
                                    ProductName = "Tofu",
                                    Quantity = 3,
                                    Discount = 0,
                                    QuantityPerUnit = "40 - 100 g pkgs.",
                                    UnitPrice = 23.25,
                                    CategoryName = "Produce",
                                    CategoryDescription = "Dried fruit and bean curd",
                                    Total = 69.75
                                },
                                new OrderDetail
                                {
                                    ProductId = 42,
                                    ProductName = "Singaporean Hokkien Fried Mee",
                                    Quantity = 5,
                                    Discount = 0,
                                    QuantityPerUnit = "32 - 1 kg pkgs.",
                                    UnitPrice = 14.0,
                                    CategoryName = "Grains/Cereals",
                                    CategoryDescription = "Breads, crackers, pasta, and cereal",
                                    Total = 70.00
                                },
                                new OrderDetail
                                {
                                    ProductId = 60,
                                    ProductName = "Camembert Pierrot",
                                    Quantity = 10,
                                    Discount = 0,
                                    QuantityPerUnit = "15 - 300 g rounds",
                                    UnitPrice = 34.00,
                                    CategoryName = "Dairy Products",
                                    CategoryDescription = "Cheeses",
                                    Total = 340.00
                                }
                            }
                        },
                        new Order
                        {
                            OrderId = 10926, // Symbol Clock
                            OrderDate = new DateTime(1998, 3, 4),
                            RequiredDate = new DateTime(1998, 4, 1),
                            ShippedDate = new DateTime(1998, 3, 11),
                            ShipperName = "Federal Shipping",
                            ShipperPhone = "(503) 555-9931",
                            Freight = 39.92,
                            Company = "Company F",
                            ShipTo = "Company F, Avda. de la Constitución 2222, 05021, México D.F., Mexico",
                            OrderTotal = 507.20,
                            Status = "Shipped",
                            SymbolCode = 57633,
                            Details = new List<OrderDetail>
                            {
                                new OrderDetail
                                {
                                    ProductId = 11,
                                    ProductName = "Queso Cabrales",
                                    Quantity = 2,
                                    Discount = 0,
                                    QuantityPerUnit = "1 kg pkg.",
                                    UnitPrice = 21.0,
                                    CategoryName = "Dairy Products",
                                    CategoryDescription = "Cheeses",
                                    Total = 42.00
                                },
                                new OrderDetail
                                {
                                    ProductId = 13,
                                    ProductName = "Konbu",
                                    Quantity = 10,
                                    Discount = 0,
                                    QuantityPerUnit = "2 kg box",
                                    UnitPrice = 6.0,
                                    CategoryName = "Seafood",
                                    CategoryDescription = "Seaweed and fish",
                                    Total = 60.00
                                },
                                new OrderDetail
                                {
                                    ProductId = 19,
                                    ProductName = "Teatime Chocolate Biscuits",
                                    Quantity = 7,
                                    Discount = 0,
                                    QuantityPerUnit = "10 boxes x 12 pieces",
                                    UnitPrice = 9.20,
                                    CategoryName = "Confections",
                                    CategoryDescription = "Desserts, candies, and sweet breads",
                                    Total = 64.40
                                },
                                new OrderDetail
                                {
                                    ProductId = 72,
                                    ProductName = "Mozzarella di Giovanni",
                                    Quantity = 10,
                                    Discount = 0,
                                    QuantityPerUnit = "24 - 200 g pkgs.",
                                    UnitPrice = 34.80,
                                    CategoryName = "Dairy Products",
                                    CategoryDescription = "Cheeses",
                                    Total = 340.80
                                }
                            }
                        }
                    }
                },
                new Customer
                {
                    CompanyId = "ANTON",
                    CompanyName = "Company Z",
                    ContactName = "Antonio Moreno",
                    ContactTitle = "Owner",
                    Address = "Mataderos  2312",
                    City = "México D.F.",
                    PostalCode = "05023",
                    Country = "Mexico",
                    Phone = "(5) 555-3932",
                    Fax = string.Empty,
                    Orders = new List<Order>
                    {
                        new Order
                        {
                            OrderId = 10507, // Symbol Contact
                            OrderDate = new DateTime(1997, 4, 15),
                            RequiredDate = new DateTime(1997, 5, 13),
                            ShippedDate = new DateTime(1997, 4, 22),
                            ShipperName = "Speedy Express",
                            ShipperPhone = "(503) 555-9831",
                            Freight = 47.45,
                            Company = "Company Z",
                            ShipTo = "Company Z, Mataderos  2312, 05023, México D.F., Mexico",
                            OrderTotal = 978.50,
                            Status = "Closed",
                            SymbolCode = 57661,
                            Details = new List<OrderDetail>
                            {
                                new OrderDetail
                                {
                                    ProductId = 43,
                                    ProductName = "Ipoh Coffee",
                                    Quantity = 15,
                                    Discount = 0.15,
                                    QuantityPerUnit = "16 - 500 g tins",
                                    UnitPrice = 46.0,
                                    CategoryName = "Beverages",
                                    CategoryDescription = "Soft drinks, coffees, teas, beers, and ales",
                                    Total = 816.00
                                },
                                new OrderDetail
                                {
                                    ProductId = 48,
                                    ProductName = "Chocolade",
                                    Quantity = 15,
                                    Discount = 0.15,
                                    QuantityPerUnit = "10 pkgs.",
                                    UnitPrice = 12.75,
                                    CategoryName = "Confections",
                                    CategoryDescription = "Desserts, candies, and sweet breads",
                                    Total = 162.50
                                }
                            }
                        },
                        new Order
                        {
                            OrderId = 10573, // Symbol Star
                            OrderDate = new DateTime(1997, 6, 19),
                            RequiredDate = new DateTime(1997, 7, 17),
                            ShippedDate = new DateTime(1997, 6, 20),
                            ShipperName = "Federal Shipping",
                            ShipperPhone = "(503) 555-9931",
                            Freight = 84.84,
                            Company = "Company Z",
                            ShipTo = "Company Z, Mataderos  2312, 05023, México D.F., Mexico",
                            OrderTotal = 2082.00,
                            Status = "Closed",
                            SymbolCode = 57619,
                            Details = new List<OrderDetail>
                            {
                                new OrderDetail
                                {
                                    ProductId = 17,
                                    ProductName = "Alice Mutton",
                                    Quantity = 18,
                                    Discount = 0,
                                    QuantityPerUnit = "20 - 1 kg tins",
                                    UnitPrice = 39.00,
                                    CategoryName = "Meat/Poultry",
                                    CategoryDescription = "Prepared meats",
                                    Total = 702.00
                                },
                                new OrderDetail
                                {
                                    ProductId = 34,
                                    ProductName = "Sasquatch Ale",
                                    Quantity = 40,
                                    Discount = 0,
                                    QuantityPerUnit = "24 - 12 oz bottles",
                                    UnitPrice = 14.0,
                                    CategoryName = "Beverages",
                                    CategoryDescription = "Soft drinks, coffees, teas, beers, and ales",
                                    Total = 560.00
                                },
                                new OrderDetail
                                {
                                    ProductId = 53,
                                    ProductName = "Perth Pasties",
                                    Quantity = 25,
                                    Discount = 0,
                                    QuantityPerUnit = "48 pieces",
                                    UnitPrice = 32.80,
                                    CategoryName = "Meat/Poultry",
                                    CategoryDescription = "Prepared meats",
                                    Total = 820.00
                                }
                            }
                        },
                        new Order
                        {
                            OrderId = 10682, // Symbol Home
                            OrderDate = new DateTime(1997, 9, 25),
                            RequiredDate = new DateTime(1997, 10, 23),
                            ShippedDate = new DateTime(1997, 10, 1),
                            ShipperName = "United Package",
                            ShipperPhone = "(503) 555-3199",
                            Freight = 36.13,
                            Company = "Company Z",
                            ShipTo = "Company Z, Mataderos  2312, 05023, México D.F., Mexico",
                            OrderTotal = 375.50,
                            Status = "Closed",
                            SymbolCode = 57615,
                            Details = new List<OrderDetail>
                            {
                                new OrderDetail
                                {
                                    ProductId = 33,
                                    ProductName = "Geitost",
                                    Quantity = 30,
                                    Discount = 0,
                                    QuantityPerUnit = "500 g",
                                    UnitPrice = 2.50,
                                    CategoryName = "Dairy Products",
                                    CategoryDescription = "Cheeses",
                                    Total = 75.00
                                },
                                new OrderDetail
                                {
                                    ProductId = 66,
                                    ProductName = "Louisiana Hot Spiced Okra",
                                    Quantity = 4,
                                    Discount = 0,
                                    QuantityPerUnit = "24 - 8 oz jars",
                                    UnitPrice = 17.00,
                                    CategoryName = "Condiments",
                                    CategoryDescription = "Sweet and savory sauces, relishes, spreads, and seasonings",
                                    Total = 68.00
                                },
                                new OrderDetail
                                {
                                    ProductId = 75,
                                    ProductName = "Rhönbräu Klosterbier",
                                    Quantity = 30,
                                    Discount = 0,
                                    QuantityPerUnit = "24 - 0.5 l bottles",
                                    UnitPrice = 7.75,
                                    CategoryName = "Beverages",
                                    CategoryDescription = "Soft drinks, coffees, teas, beers, and ales",
                                    Total = 232.50
                                }
                            }
                        }
                    }
                }
            };
   }

   public async Task<IEnumerable<Customer>> GetCustomersAsync()
   {
      await Task.Delay(750).ConfigureAwait(false);
      return _allCompaniesOrdersAndOrderDetails;
   }

   public async Task<IEnumerable<Order>> GetOrdersAsync(string companyId)
   {
      await Task.Delay(750).ConfigureAwait(false);
      Debug.WriteLine("GetOrdersAsync");
      return _allCompaniesOrdersAndOrderDetails.FirstOrDefault(x => string.Equals(x.CompanyId, companyId, StringComparison.OrdinalIgnoreCase))?.Orders.AsEnumerable();
   }

   public async Task<Order> GetOrderAsync(string companyId, long orderId)
   {
      await Task.Delay(750).ConfigureAwait(false);
      return _allCompaniesOrdersAndOrderDetails.FirstOrDefault(x => string.Equals(x.CompanyId, companyId, StringComparison.OrdinalIgnoreCase))?.Orders.FirstOrDefault(x => x.OrderId == orderId);
   }

   public async Task<IEnumerable<OrderDetail>> GetOrderDetailsAsync(long orderId)
   {
      await Task.Delay(750).ConfigureAwait(false);
      return _allCompaniesOrdersAndOrderDetails.SelectMany(x => x.Orders).FirstOrDefault(x => x.OrderId == orderId)?.Details.AsEnumerable();
   }
}
