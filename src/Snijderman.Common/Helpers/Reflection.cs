using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Snijderman.Common.Helpers;

public static class Reflection
{
   public static object MakeGenericMethodAndInvoke(object objectInstance, string methodName, Type genericType, object[] parameters = default) => MakeGenericMethodAndInvoke(objectInstance, methodName, [genericType], parameters);

   public static T MakeGenericMethodAndInvoke<T>(object objectInstance, string methodName, Type genericType, object[] parameters = default) => (T)MakeGenericMethodAndInvoke(objectInstance, methodName, [genericType], parameters);

   public static object MakeGenericMethodAndInvoke(object objectInstance, string methodName, Type[] typeArguments, object[] parameters = default)
   {
      var method = FindMethodToInvoke(objectInstance, methodName, typeArguments, parameters);

      return method.Invoke(objectInstance, parameters);
   }

   public static T MakeGenericMethodAndInvoke<T>(object objectInstance, string methodName, Type[] typeArguments, object[] parameters = default) => (T)MakeGenericMethodAndInvoke(objectInstance, methodName, typeArguments, parameters);

   public static Task<object> MakeGenericMethodAndInvokeAsync(object objectInstance, string methodName, Type genericType, object[] parameters = default) => MakeGenericMethodAndInvokeAsync(objectInstance, methodName, [genericType], parameters);

   public static async Task<object> MakeGenericMethodAndInvokeAsync(object objectInstance, string methodName, Type[] typeArguments, object[] parameters = default)
   {
      var method = FindMethodToInvoke(objectInstance, methodName, typeArguments, parameters);

      var result = method.Invoke(objectInstance, parameters);
      if (result is not Task task)
      {
         return result;
      }

      await task.ConfigureAwait(false);
      var resultProperty = task.GetType().GetProperty("Result");
      return resultProperty.GetValue(task);
   }

   public static Task<T> MakeGenericMethodAndInvokeAsync<T>(object objectInstance, string methodName, Type genericType, object[] parameters = default) => MakeGenericMethodAndInvokeAsync<T>(objectInstance, methodName, [genericType], parameters);

   public static async Task<T> MakeGenericMethodAndInvokeAsync<T>(object objectInstance, string methodName, Type[] typeArguments, object[] parameters = default)
   {
      var method = FindMethodToInvoke(objectInstance, methodName, typeArguments, parameters);

      var result = method.Invoke(objectInstance, parameters);
      if (result is not Task<T> task)
      {
         return (T)result;
      }
      await task.ConfigureAwait(false);
      return task.Result;
   }

   private static MethodInfo FindMethodToInvoke(object objectInstance, string methodName, Type[] providedGenericTypes, params object[] providedParameters)
   {
      ArgumentNullException.ThrowIfNull(objectInstance);

      if (string.IsNullOrWhiteSpace(methodName))
      {
         throw new ArgumentException("Value cannot be null or whitespace.", nameof(methodName));
      }

      ArgumentNullException.ThrowIfNull(providedGenericTypes);
      if (providedGenericTypes.Length == 0)
      {
         throw new ArgumentNullException(nameof(providedGenericTypes));
      }

      for (var i = 0; i < providedGenericTypes.Length; i++)
      {
         if (providedGenericTypes[i] == null)
         {
            throw new ArgumentNullException(nameof(providedGenericTypes));
         }
      }

      // Step 1: Get the type of the instance
      var instanceType = objectInstance.GetType();

      // Step 2: Get all methods with the specified name, that are generic and have the specified number of generic arguments
      var potentialMethods = instanceType.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                                         .Where(m => m.Name == methodName && m.IsGenericMethodDefinition && m.GetGenericArguments().Length == providedGenericTypes?.Length)
                                         .ToList();

      if (potentialMethods.Count == 0)
      {
         throw new InvalidOperationException($"No method(s) found with name {methodName} that can accept {providedGenericTypes.Length} generic arguments.");
      }

      // Step 3: Filter methods by the number of generic arguments
      var methodsToInvoke = FindMethodsToInvoke(potentialMethods, providedGenericTypes, providedParameters).ToList();

      return methodsToInvoke.Count switch
      {
         0   => throw new InvalidOperationException($"No suitable method(s) found with name {methodName} that can be invoked."),
         _   => methodsToInvoke[0] // in case of multiple methods, we just take the first one, the response is sorted by the number of generic parameters
      };
   }

   private static IReadOnlyList<MethodInfo> FindMethodsToInvoke(IReadOnlyList<MethodInfo> potentialMethods, Type[] providedGenericTypes, object[] providedParameters)
   {
      var methodsToInvoke = new List<(MethodInfo Method, int GenericParametersCount)>();
      for (var i = 0; i < potentialMethods.Count; i++)
      {
         var methodToInvoke = CanMethodBeInvoked(potentialMethods[i], providedGenericTypes, providedParameters);
         if (methodToInvoke != default)
         {
            methodsToInvoke.Add(methodToInvoke);
         }
      }

      // sort the methods by the number of generic parameters
      return methodsToInvoke.OrderByDescending(x => x.GenericParametersCount).Select(x => x.Method).ToList();
   }

   private static (MethodInfo Method, int GenericParametersCount) CanMethodBeInvoked(MethodInfo method, Type[] providedGenericTypes, object[] providedParameters)
   {
      try
      {
         // Make the method generic (this will fail if the provided types are not compatible with the method's constraints
         var genericMethod = method.MakeGenericMethod(providedGenericTypes);
         var genericParameters = new List<string>();

         // Check the expected parameter types of the method
         var providedParametersLength = providedParameters?.Length ?? 0;
         var genericParameterInfos = genericMethod.GetParameters();
         if (providedParametersLength != genericParameterInfos.Length)
         {
            return default;
         }

         var typeParametersInfos = method.GetParameters();

         // Validate passed parameters
         for (var i = 0; i < providedParametersLength; i++)
         {
            if (providedParameters![i] == null)
            {
               continue;
            }

            var expectedTypeFromDefinition = genericParameterInfos[i].ParameterType;
            var actualProvidedType = providedParameters[i].GetType();

            if (!expectedTypeFromDefinition.IsAssignableFrom(actualProvidedType))
            {
               return default;
            }

            //var expectedTypeFromDefinitionIsGeneric = expectedTypeFromDefinition.IsGenericType;
            var typeParametersInfosParameterType = typeParametersInfos[i].ParameterType;
            var typeParametersInfosIsGeneric = typeParametersInfos[i].ParameterType.IsGenericType;


            if (typeParametersInfosParameterType.IsGenericParameter)
            {
               genericParameters.Add(typeParametersInfosParameterType.Name);
            }
         }

         // Looks like this method can be invoked
         var genericParametersCount = genericParameters.Distinct().Count();
         return (genericMethod, genericParametersCount);
      }
      catch (Exception exc)
      {
         Console.WriteLine(exc);
         return default;
      }
   }
}
