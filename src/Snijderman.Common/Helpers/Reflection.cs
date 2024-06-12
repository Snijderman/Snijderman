using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Snijderman.Common.Helpers;

public static class Reflection
{
   public static object MakeGenericMethodAndInvoke(object objectInstance, string methodName, Type genericType, object[] parameters = default)
      => MakeGenericMethodAndInvoke(objectInstance, methodName, [genericType], parameters);

   public static T MakeGenericMethodAndInvoke<T>(object objectInstance, string methodName, Type genericType, object[] parameters = default)
      => (T)MakeGenericMethodAndInvoke(objectInstance, methodName, [genericType], parameters);

   public static object MakeGenericMethodAndInvoke(object objectInstance, string methodName, Type[] typeArguments, object[] parameters = default)
   {
      var method = FindMethodToInvoke(objectInstance, methodName, typeArguments, parameters);

      return method.MakeGenericMethod(typeArguments).Invoke(objectInstance, parameters);
   }

   public static T MakeGenericMethodAndInvoke<T>(object objectInstance, string methodName, Type[] typeArguments, object[] parameters = default)
      => (T)MakeGenericMethodAndInvoke(objectInstance, methodName, typeArguments, parameters);

   public static Task<object> MakeGenericMethodAndInvokeAsync(object objectInstance, string methodName, Type genericType, object[] parameters = default)
      => MakeGenericMethodAndInvokeAsync(objectInstance, methodName, [genericType], parameters);

   public static async Task<object> MakeGenericMethodAndInvokeAsync(object objectInstance, string methodName, Type[] typeArguments, object[] parameters = default)
   {
      var method = FindMethodToInvoke(objectInstance, methodName, typeArguments, parameters);

      var task = (Task)method.MakeGenericMethod(typeArguments).Invoke(objectInstance, parameters);
      await task.ConfigureAwait(false);
      var resultProperty = task.GetType().GetProperty("Result");
      return resultProperty.GetValue(task);
   }

   public static Task<T> MakeGenericMethodAndInvokeAsync<T>(object objectInstance, string methodName, Type genericType, object[] parameters = default)
      => MakeGenericMethodAndInvokeAsync<T>(objectInstance, methodName, [genericType], parameters);

   public static async Task<T> MakeGenericMethodAndInvokeAsync<T>(object objectInstance, string methodName, Type[] typeArguments, object[] parameters = default)
   {
      var method = FindMethodToInvoke(objectInstance, methodName, typeArguments, parameters);

      var result = (Task<T>)method.MakeGenericMethod(typeArguments).Invoke(objectInstance, parameters);
      await result.ConfigureAwait(false);
      return result.Result;
   }

   private static MethodInfo FindMethodToInvoke(object objectInstance, string methodName, Type[] typeArguments, object[] parameters)
   {
      ArgumentNullException.ThrowIfNull(objectInstance);

      if (string.IsNullOrWhiteSpace(methodName))
      {
         throw new ArgumentException("Value cannot be null or whitespace.", nameof(methodName));
      }

      ArgumentNullException.ThrowIfNull(typeArguments);
      if (typeArguments.Length == 0)
      {
         throw new ArgumentNullException(nameof(typeArguments));
      }
      for (var i = 0; i < typeArguments.Length; i++)
      {
         if (typeArguments[i] == null)
         {
            throw new ArgumentNullException(nameof(typeArguments));
         }
      }

      var method = GetMethodToInvoke(objectInstance, methodName, typeArguments, parameters);
      if (method == default)
      {
         throw new InvalidOperationException($"Method '{methodName}' not found for type '{objectInstance.GetType()}'");
      }

      return method;
   }

   private static MethodInfo GetMethodToInvoke(object objectInstance, string methodName, Type[] typeArguments, object[] parameters)
   {
      var methods = objectInstance.GetType()
#pragma warning disable S3011 // Reflection should not be used to increase accessibility of classes, methods, or fields
                                  .GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static)
                                  .Where(m => m.Name == methodName && m.IsGenericMethod && m.GetGenericArguments().Length == typeArguments.Length);
#pragma warning restore S3011 // Reflection should not be used to increase accessibility of classes, methods, or fields

      var result = FindMethodsToInvoke(methods, typeArguments, parameters).ToList();

      return result.Count switch
      {
         0 => default,
         1 => result[0],
         _ => GetMethodToInvoke(result, typeArguments)
      };
   }

   private static MethodInfo GetMethodToInvoke(List<MethodInfo> methods, Type[] typeArguments)
   {
      //arbitrary, but let's see if there is a method with the same number of generic parameters as are provided
      var methodToReturn = methods.FirstOrDefault(x => x.GetParameters().Count(x => x.ParameterType.IsGenericParameter) == typeArguments.Length);
      return methodToReturn ?? methods.First();
   }

   private static IEnumerable<MethodInfo> FindMethodsToInvoke(IEnumerable<MethodInfo> methods, Type[] typeArguments, object[] parameters)
   {
      return methods.Where(m => MethodCanBeUsed(m.GetGenericArguments(), typeArguments, m.GetParameters(), parameters));
   }

   private static bool MethodCanBeUsed(Type[] genericArguments, Type[] typeArguments, ParameterInfo[] parameterInfo, object[] parameters)
   {
      var parameterInfoLength = parameterInfo?.Length ?? 0;
      var parametersLength = parameters?.Length ?? 0;
      if (parameterInfoLength == 0 && parametersLength == 0)
      {
         return true;
      }
      if (parameterInfoLength != parametersLength)
      {
         return false;
      }

      for (var i = 0; i < parameterInfoLength; i++)
      {
         if (parameters?[i] == default)
         {
            continue; // lets assume default is a valid argument
         }

         var parameterInfoType = GetParameterInfoType(parameterInfo[i], genericArguments, typeArguments);
         if (parameterInfoType == default)
         {
            return false;
         }
         var parameterType = parameters[i].GetType();
         if (parameterInfoType != parameterType && !parameterInfoType.IsAssignableFrom(parameterType))
         {
            return false;
         }
      }

      return true;

   }

   private static Type GetParameterInfoType(ParameterInfo parameterInfo, Type[] genericArguments, Type[] typeArguments)
   {
      if (!parameterInfo.ParameterType.IsGenericParameter)
      {
         return parameterInfo.ParameterType;
      }

      // now determine the type based on provided list of types
      for (var i = 0; i < genericArguments.Length; i++)
      {
         if (parameterInfo.ParameterType == genericArguments[i])
         {
            return typeArguments[i];
         }
      }

      return default;
   }
}
