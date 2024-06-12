using System.Collections.Generic;
using Snijderman.Samples.Common.Model;

namespace Snijderman.Wpf.MVVM.Example.ViewModels;

public class CustomerViewModel : WpfMvvmViewModelBase
{
   public string CompanyId { get; set; }

   public string CompanyName { get; set; }

   public ICollection<Order> Orders { get; set; } = new List<Order>();

   public bool IsEnabled => this.Orders?.Count > 0;

   public bool IsVisible { get; set; } = true;
}
