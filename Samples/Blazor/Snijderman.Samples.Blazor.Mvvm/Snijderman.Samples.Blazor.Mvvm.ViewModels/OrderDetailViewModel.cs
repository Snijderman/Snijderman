using Snijderman.Common.Mvvm;

namespace Snijderman.Samples.Blazor.Mvvm.ViewModels
{
   public class OrderDetailViewModel : ViewModelBase
   {
      private long _productId;
      public long ProductId
      {
         get => this._productId;
         set => this.Set(ref this._productId, value);
      }

      private string _productName;
      public string ProductName
      {
         get => this._productName;
         set => this.Set(ref this._productName, value);
      }

      private int _quantity;
      public int Quantity
      {
         get => this._quantity;
         set => this.Set(ref this._quantity, value);
      }

      private double _discount;
      public double Discount
      {
         get => this._discount;
         set => this.Set(ref this._discount, value);
      }

      private string _quantityPerUnit;
      public string QuantityPerUnit
      {
         get => this._quantityPerUnit;
         set => this.Set(ref this._quantityPerUnit, value);
      }

      private double _unitPrice;
      public double UnitPrice
      {
         get => this._unitPrice;
         set => this.Set(ref this._unitPrice, value);
      }

      private string _categoryName;
      public string CategoryName
      {
         get => this._categoryName;
         set => this.Set(ref this._categoryName, value);
      }

      private string _categoryDescription;
      public string CategoryDescription
      {
         get => this._categoryDescription;
         set => this.Set(ref this._categoryDescription, value);
      }

      private double _total;
      public double Total
      {
         get => this._total;
         set => this.Set(ref this._total, value);
      }

      public string ShortDescription => $"Product ID: {this.ProductId} - {this.ProductName}";
   }
}
