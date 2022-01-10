using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Snijderman.Common.ExtensionMethods
{
   public static class LambdaExtensions
   {
      public static void SetProperty<T, TValue>(this T target, Expression<Func<T, TValue>> memberLamda, TValue value)
      {
         if (memberLamda == null)
         {
            throw new ArgumentNullException(nameof(memberLamda));
         }

         if (memberLamda.Body is not MemberExpression memberSelectorExpression)
         {
            return;
         }

         var property = memberSelectorExpression.Member as PropertyInfo;
         if (property != null)
         {
            property.SetValue(target, value, null);
         }
      }
   }
}
