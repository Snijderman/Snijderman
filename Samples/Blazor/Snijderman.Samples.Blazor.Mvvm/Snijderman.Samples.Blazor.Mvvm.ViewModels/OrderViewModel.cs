using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Snijderman.Common.Mvvm;
using Snijderman.Samples.Common.Model;

namespace Snijderman.Samples.Blazor.Mvvm.ViewModels;

public class OrderViewModel : ViewModelBase
{
   private long _orderId;
   public long OrderId
   {
      get => this._orderId;
      set
      {
         if (this.Set(ref this._orderId, value))
         {
            this.IsValid = this.OrderId == 1512;
         }
      }
   }

   private DateTime _orderDate;
   public DateTime OrderDate
   {
      get => this._orderDate;
      set => this.Set(ref this._orderDate, value);
   }

   private DateTime _requiredDate;
   public DateTime RequiredDate
   {
      get => this._requiredDate;
      set => this.Set(ref this._requiredDate, value);
   }

   private DateTime _shippedDate;
   public DateTime ShippedDate
   {
      get => this._shippedDate;
      set => this.Set(ref this._shippedDate, value);
   }

   private string _shipperName;
   public string ShipperName
   {
      get => this._shipperName;
      set => this.Set(ref this._shipperName, value);
   }

   private string _shipperPhone;
   public string ShipperPhone
   {
      get => this._shipperPhone;
      set => this.Set(ref this._shipperPhone, value);
   }


   private double _freight;
   public double Freight
   {
      get => this._freight;
      set => this.Set(ref this._freight, value);
   }

   private string _company;
   public string Company
   {
      get => this._company;
      set => this.Set(ref this._company, value);
   }

   private string _shipTo;
   public string ShipTo
   {
      get => this._shipTo;
      set => this.Set(ref this._shipTo, value);
   }

   private double _orderTotal;
   public double OrderTotal
   {
      get => this._orderTotal;
      set => this.Set(ref this._orderTotal, value);
   }

   private string _status;
   public string Status
   {
      get => this._status;
      set => this.Set(ref this._status, value);
   }

   private char _symbol;
   public char Symbol
   {
      get => this._symbol;
      set => this.Set(ref this._symbol, value);
   }

   private int _symbolCode;
   public int SymbolCode
   {
      get => this._symbolCode;
      set => this.Set(ref this._symbolCode, value);
   }

   private ObservableCollection<OrderDetailViewModel> _details = new();
   public ObservableCollection<OrderDetailViewModel> Details
   {
      get => this._details;
      set => this.Set(ref this._details, value);
   }

   public override string ToString() => $"{this.Company} {this.Status}";

   public string ShortDescription => $"Order ID: {this.OrderId}";

   public override Task LoadAsync()
   {
      this.IsLoaded = true;
      return Task.CompletedTask;
   }

   private bool _isLoaded;
   public bool IsLoaded
   {
      get => this._isLoaded;
      set => this.Set(ref this._isLoaded, value);
   }

   private bool _isValid;
   public bool IsValid
   {
      get => this._isValid;
      set => this.Set(ref this._isValid, value);
   }

   public bool DisableSave => !this.IsValid;
   //{
   //   get => this._isValid;
   //   set => this.Set(ref this._isValid, value);
   //}
}
