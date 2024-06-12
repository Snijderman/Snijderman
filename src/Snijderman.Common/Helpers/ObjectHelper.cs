using System;
using System.Linq;
using System.Reflection;

namespace Snijderman.Common.Helpers;

public static class ObjectHelper
{
   public static bool HasProperty(this object instance, string propertyName)
   {
      if (instance is null)
      {
         throw new ArgumentNullException(nameof(instance));
      }

      var property = GetPropertyInfo(instance, propertyName);

      return property != null;
   }

   public static object GetPropertyValue(this object instance, string propertyName)
   {
      return GetPropertyValue(instance, GetPropertyInfo(instance, propertyName));
   }

   public static object GetPropertyValue(this object instance, string propertyName, BindingFlags bindingFlags)
   {
      return GetPropertyValue(instance, GetPropertyInfo(instance, propertyName, bindingFlags));
   }

   public static object GetPropertyValue(this object instance, PropertyInfo property)
   {
      if (instance is null)
      {
         throw new ArgumentNullException(nameof(instance));
      }

      if (property is null)
      {
         throw new ArgumentNullException(nameof(property));
      }

      return property.GetValue(instance);
   }

   public static PropertyInfo GetPropertyInfo(object instance, string propertyName)
   {
      if (instance is null)
      {
         throw new ArgumentNullException(nameof(instance));
      }

      if (string.IsNullOrWhiteSpace(propertyName))
      {
         throw new ArgumentException($"'{nameof(propertyName)}' cannot be null or empty.", nameof(propertyName));
      }

      return GetPropertyInfo(instance, propertyName, BindingFlags.Public | BindingFlags.Instance);
   }

   public static PropertyInfo GetPropertyInfo(object instance, string propertyName, BindingFlags bindingFlags)
   {
      if (instance is null)
      {
         throw new ArgumentNullException(nameof(instance));
      }

      if (string.IsNullOrWhiteSpace(propertyName))
      {
         throw new ArgumentException($"'{nameof(propertyName)}' cannot be null or empty.", nameof(propertyName));
      }

      var objectProperties = instance.GetType().GetProperties(bindingFlags);

      return objectProperties.FirstOrDefault(x => x.Name == propertyName);
   }
}
