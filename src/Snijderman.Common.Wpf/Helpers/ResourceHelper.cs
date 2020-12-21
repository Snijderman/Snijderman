using System.Windows;

namespace Snijderman.Common.Wpf.Helpers
{
   public static class ResourceHelper
   {
      public static FrameworkElement ResourceOwnerFallback;
      public static object TryFindResource(IFrameworkInputElement service, object resourceKey)
      {
         if (service is FrameworkElement element)
         {
            return element.TryFindResource(resourceKey);
         }

         if (ResourceOwnerFallback != null)
         {
            return ResourceOwnerFallback.TryFindResource(resourceKey);
         }

         return Application.Current?.TryFindResource(resourceKey);
      }

   }
}
