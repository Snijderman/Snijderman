using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using Snijderman.Common.Wpf.Themes.Default;

namespace Snijderman.Common.Wpf.MarkupExtensions;

public sealed class SpaceExtension : MarkupExtension
{
   private static double? _cachedHorizontalSpace;

   private static double? _cachedVerticalSpace;

   private static FrameworkElement _resourceOwnerFallback;

   public double? Factor { get; set; }

   public double Left { get; set; }

   public double Top { get; set; }

   public double Right { get; set; }

   public double Bottom { get; set; }

   public double? Offset { get; set; }

   public double OffsetLeft { get; set; }

   public double OffsetTop { get; set; }

   public double OffsetRight { get; set; }

   public double OffsetBottom { get; set; }

   public Orientation? Orientation { get; set; }

   public double HorizontalOffset
   {
      set
      {
         this.OffsetLeft = value;
         this.OffsetRight = value;
      }
   }

   public double VerticalOffset
   {
      set
      {
         this.OffsetTop = value;
         this.OffsetBottom = value;
      }
   }

   public SpaceExtension()
   {
      this.Factor = 1;
      this.Left = 1;
      this.Top = 1;
      this.Right = 1;
      this.Bottom = 1;
   }

   public SpaceExtension(string expression)
   {
      if (!this.TryParseExpression(expression, out var factor, out var offset))
      {
         throw new NotSupportedException("Expression cannot be parsed.");
      }

      this.Factor = factor;
      this.Left = factor;
      this.Top = factor;
      this.Right = factor;
      this.Bottom = factor;
      this.Offset = offset;
      this.OffsetLeft = offset;
      this.OffsetTop = offset;
      this.OffsetRight = offset;
      this.OffsetBottom = offset;
   }

   public SpaceExtension(string horizontalExpression, string verticalExpression)
   {
      var canParseHorizontal = this.TryParseExpression(horizontalExpression, out var horizontalFactor, out var horizontalOffset);
      var canParseVertical = this.TryParseExpression(verticalExpression, out var verticalFactor, out var verticalOffset);

      if (!canParseHorizontal || !canParseVertical)
      {
         throw new NotSupportedException("Expression cannot be parsed.");
      }

      this.Left = horizontalFactor;
      this.Top = verticalFactor;
      this.Right = horizontalFactor;
      this.Bottom = verticalFactor;

      this.OffsetLeft = horizontalOffset;
      this.OffsetTop = verticalOffset;
      this.OffsetRight = horizontalOffset;
      this.OffsetBottom = verticalOffset;
   }

   public SpaceExtension(string leftExpression, string topExpression, string rightExpression, string bottomExpression)
   {
      var canParseLeft = this.TryParseExpression(leftExpression, out var leftFactor, out var leftOffset);
      var canParseTop = this.TryParseExpression(topExpression, out var topFactor, out var topOffset);
      var canParseRight = this.TryParseExpression(rightExpression, out var rightFactor, out var rightOffset);
      var canParseBottom = this.TryParseExpression(bottomExpression, out var bottomFactor, out var bottomOffset);

      if (!canParseLeft || !canParseTop || !canParseRight || !canParseBottom)
      {
         throw new NotSupportedException("Expression cannot be parsed.");
      }

      this.Left = leftFactor;
      this.Top = topFactor;
      this.Right = rightFactor;
      this.Bottom = bottomFactor;

      this.OffsetLeft = leftOffset;
      this.OffsetTop = topOffset;
      this.OffsetRight = rightOffset;
      this.OffsetBottom = bottomOffset;
   }

   public static void SetSpace(double horizontalSpace, double verticalSpace)
   {
      _cachedHorizontalSpace = horizontalSpace;
      _cachedVerticalSpace = verticalSpace;
   }

   public static void SetSpaceResourceOwnerFallback(FrameworkElement resourceOwner)
   {
      _resourceOwnerFallback = resourceOwner;
   }

   private static void ReleaseSpaceResourceOwnerFallback()
   {
      _resourceOwnerFallback = null;
   }

   /// <summary>
   /// Expects an expression in the form of [x+y] or [x-y] where x is parsed as factor and y as offset
   /// </summary>
   private bool TryParseExpression(string expression, out double factor, out double offset)
   {
      factor = 0;
      offset = 0;

      if (string.IsNullOrEmpty(expression))
      {
         return false;
      }

      char sign;

      if (expression[1..].Contains('+'))
      {
         sign = '+';
      }
      else if (expression[1..].Contains('-'))
      {
         sign = '-';
      }
      else
      {
         return double.TryParse(expression, NumberStyles.Any, CultureInfo.InvariantCulture, out factor);
      }

      var expressionParts = expression.Split(sign);

      if (expressionParts[0] == string.Empty) // first char was <sign>
      {
         expressionParts = new[]
         {
               '-' + expressionParts[1], expressionParts[2]
            };
      }


      if (expressionParts.Length != 2)
      {
         return false;
      }

      var canParseFactor = double.TryParse(expressionParts[0], NumberStyles.Any, CultureInfo.InvariantCulture, out factor);
      var canParseOffset = double.TryParse(expressionParts[1], NumberStyles.Any, CultureInfo.InvariantCulture, out offset);

      if (sign == '-')
      {
         offset *= -1;
      }

      return canParseFactor && canParseOffset;
   }

   public override object ProvideValue(IServiceProvider serviceProvider)
   {
      var service = (IProvideValueTarget)serviceProvider.GetService(typeof(IProvideValueTarget));
      var targetProperty = this.GetTargetProperty(service);
      var horizontalSpace = this.GetHorizontalSpace(service);
      var verticalSpace = this.GetVerticalSpace(service);

      if (targetProperty != null && targetProperty.PropertyType == typeof(GridLength) && this.Factor.HasValue && this.Offset.HasValue)
      {
         if (this.Orientation.HasValue)
         {
            return this.Factor * (this.Orientation.Value == System.Windows.Controls.Orientation.Horizontal ? horizontalSpace : verticalSpace) + this.Offset;
         }

         if (targetProperty.OwnerType == typeof(RowDefinition))
         {
            return new GridLength(this.Factor.Value * verticalSpace + this.Offset.Value);
         }

         return new GridLength(this.Factor.Value * horizontalSpace + this.Offset.Value);
      }

      if (targetProperty != null && targetProperty.PropertyType == typeof(Thickness) || !this.Factor.HasValue)
      {
         return new Thickness(this.Left * horizontalSpace + this.OffsetLeft, this.Top * verticalSpace + this.OffsetTop, this.Right * horizontalSpace + this.OffsetRight, this.Bottom * verticalSpace + this.OffsetBottom);
      }

      if (this.Orientation.HasValue)
      {
         return this.Factor * (this.Orientation.Value == System.Windows.Controls.Orientation.Horizontal ? horizontalSpace : verticalSpace) + this.Offset;
      }

      var guessedOrientation = this.GuessPreferredOrientation(targetProperty);
      if (guessedOrientation != null)
      {
         return this.Factor * (guessedOrientation.Value == System.Windows.Controls.Orientation.Horizontal ? horizontalSpace : verticalSpace) + this.Offset;
      }

      throw new InvalidOperationException($"Cannot determine target orientation for property ${service.TargetProperty} on type ${service.TargetObject.GetType().FullName}. Orientation must be specified manually.");
   }

   private DependencyProperty GetTargetProperty(IProvideValueTarget service)
   {
      var targetProperty = service.TargetProperty as DependencyProperty;

      if (service.TargetObject is Setter setter)
      {
         targetProperty = setter.Property;
      }

      return targetProperty;
   }

   private double GetHorizontalSpace(IProvideValueTarget service)
   {
      if (_cachedHorizontalSpace.HasValue)
      {
         return _cachedHorizontalSpace.Value;
      }

      this.FindAndCacheSpaceResources(service);

      if (!_cachedHorizontalSpace.HasValue)
      {
         throw new Exception("Dimensions.HorizontalSpace could not be retrieved.");
      }

      return _cachedHorizontalSpace.Value;
   }

   private double GetVerticalSpace(IProvideValueTarget service)
   {
      if (_cachedVerticalSpace.HasValue)
      {
         return _cachedVerticalSpace.Value;
      }

      this.FindAndCacheSpaceResources(service);

      if (!_cachedVerticalSpace.HasValue)
      {
         throw new Exception("Dimensions.VerticalSpace could not be retrieved.");
      }

      return _cachedVerticalSpace.Value;
   }

   private void FindAndCacheSpaceResources(IProvideValueTarget service)
   {
      var horizontalSpace = this.TryFindResource(service, Dimensions.HorizontalSpace);
      var verticalSpace = this.TryFindResource(service, Dimensions.VerticalSpace);

      _cachedHorizontalSpace = (double?)horizontalSpace ?? throw new ResourceReferenceKeyNotFoundException("Cannot find Dimensions.HorizontalSpace resource.", Dimensions.HorizontalSpace);
      _cachedVerticalSpace = (double?)verticalSpace ?? throw new ResourceReferenceKeyNotFoundException("Cannot find Dimensions.VerticalSpace resource.", Dimensions.VerticalSpace);

      ReleaseSpaceResourceOwnerFallback();
   }

   private object TryFindResource(IProvideValueTarget service, object resourceKey)
   {
      if (service.TargetObject is FrameworkElement element)
      {
         return element.TryFindResource(resourceKey);
      }

      if (_resourceOwnerFallback != null)
      {
         return _resourceOwnerFallback.TryFindResource(resourceKey);
      }

      return Application.Current?.TryFindResource(resourceKey);
   }

   private Orientation? GuessPreferredOrientation(DependencyProperty targetProperty)
   {
      if (targetProperty == null)
      {
         return null;
      }

      string[] horizontalPropertyNames = { "width", "horizontal" };
      string[] verticalPropertyNames = { "height", "vertical" };

      if (horizontalPropertyNames.Any(name => targetProperty.Name.ToLower().Contains(name)))
      {
         return System.Windows.Controls.Orientation.Horizontal;
      }

      if (verticalPropertyNames.Any(name => targetProperty.Name.ToLower().Contains(name)))
      {
         return System.Windows.Controls.Orientation.Vertical;
      }

      return null;
   }
}
