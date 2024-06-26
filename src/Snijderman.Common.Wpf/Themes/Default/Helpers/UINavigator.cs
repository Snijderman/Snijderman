using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace Snijderman.Common.Wpf.Themes.Default.Helpers;

public class UiNavigator
{
   public static T FindVisualChild<T>(DependencyObject depObj) where T : DependencyObject
   {
      return FindVisualChildren<T>(depObj).FirstOrDefault();
   }

   public static T FindVisualChild<T>(DependencyObject depObj, string name) where T : DependencyObject
   {
      return FindVisualChildren<T>(depObj).OfType<FrameworkElement>().FirstOrDefault(x => x.Name == name) as T;
   }

   public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
   {
      if (depObj == null)
      {
         yield break;
      }

      for (var i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
      {
         var child = VisualTreeHelper.GetChild(depObj, i);
         if (child is T variable)
         {
            yield return variable;
         }

         foreach (var childOfChild in FindVisualChildren<T>(child))
         {
            yield return childOfChild;
         }
      }
   }

   public static T FindVisualParent<T>(DependencyObject current) where T : DependencyObject
   {
      current = VisualTreeHelper.GetParent(current);

      while (current != null)
      {
         if (current is T t)
         {
            return t;
         }

         current = VisualTreeHelper.GetParent(current);
      };

      return null;
   }
}
