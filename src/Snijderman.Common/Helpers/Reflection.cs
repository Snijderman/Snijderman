using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Snijderman.Common.Helpers;

public static class Reflection
{
   public static object MakeGenericMethodAndInvoke(object objectInstance, string methodName, Type genericType, object[] parameters = default)
   {
      if (genericType == null)
      {
         throw new ArgumentNullException(nameof(genericType));
      }

      if (objectInstance == null)
      {
         throw new ArgumentNullException(nameof(objectInstance));
      }

      if (string.IsNullOrWhiteSpace(methodName))
      {
         throw new ArgumentException("Value cannot be null or whitespace.", nameof(methodName));
      }

      var method = GetMethodToInvoke(objectInstance, methodName, new[] { genericType }, parameters);
      if (method == default)
      {
         throw new InvalidOperationException($"Method '{methodName}' not found for type '{objectInstance.GetType()}'");
      }

      return method.MakeGenericMethod(genericType).Invoke(objectInstance, parameters);
   }

   public static object MakeGenericMethodAndInvoke(object objectInstance, string methodName, Type[] typeArguments, object[] parameters = default)
   {
      if (typeArguments == null)
      {
         throw new ArgumentNullException(nameof(typeArguments));
      }

      if (objectInstance == null)
      {
         throw new ArgumentNullException(nameof(objectInstance));
      }

      if (string.IsNullOrWhiteSpace(methodName))
      {
         throw new ArgumentException("Value cannot be null or whitespace.", nameof(methodName));
      }

      var method = GetMethodToInvoke(objectInstance, methodName, typeArguments, parameters);
      if (method == default)
      {
         throw new InvalidOperationException($"Method '{methodName}' not found for type '{objectInstance.GetType()}'");
      }

      return method.MakeGenericMethod(typeArguments).Invoke(objectInstance, parameters);
   }

   private static MethodInfo GetMethodToInvoke(object objectInstance, string methodName, Type[] typeArguments, object[] parameters)
   {
      var methods = objectInstance.GetType()
                                  .GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static)
                                  .Where(m => m.Name == methodName && m.IsGenericMethod && m.GetGenericArguments().Length == typeArguments.Length);

      var result = FindMethodsToInvoke(methods, typeArguments, parameters).ToList();

      return result.Count switch
      {
         0 => default,
         1 => result[0],
         _ => GetMethodToInvoke(result, typeArguments)
      };
   }

   private static IEnumerable<MethodInfo> FindMethodsToInvoke(IEnumerable<MethodInfo> methods, Type[] typeArguments, object[] parameters)
   {
      return methods.Where(m => MethodCanBeUsed(m.GetGenericArguments(), typeArguments, m.GetParameters(), parameters));
   }

   private static bool MethodCanBeUsed(Type[] genericArguments, Type[] typeArguments, ParameterInfo[] parameterInfo, object[] parameters)
   {
      var parameterInfoLength = parameterInfo == null ? 0 : parameterInfo.Length;
      var parametersLength = parameters == null ? 0 : parameters.Length;
      if (parameterInfoLength == 0 && parametersLength == 0)
      {
         return true;
      }
      else if (parameterInfoLength != parametersLength)
      {
         return false;
      }

      for (var i = 0; i < parameterInfo.Length; i++)
      {
         if (parameters[i] == default)
         {
            continue; // lets assume default is a valid argument
         }

         var parameterInfoType = GetParameterInfoType(parameterInfo[i], genericArguments, typeArguments);
         if (parameterInfoType == default)
         {
            return false;
         }
         var parameterType = parameters[i].GetType();
         if (!parameterInfoType.Equals(parameterType) && !parameterInfoType.IsAssignableFrom(parameterType))
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

   private static MethodInfo GetMethodToInvoke(List<MethodInfo> methods, Type[] typeArguments)
   {
      //arbitrary, but let's see if there is a method with the same number of generic parameters as are provided
      var methodToReturn = methods.FirstOrDefault(x => x.GetParameters().Where(x => x.ParameterType.IsGenericParameter).Count() == typeArguments.Length);
      return methodToReturn ?? methods.First();
   }
}
