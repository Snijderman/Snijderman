using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Snijderman.Common.Utils
{
   public static class ObjectActivator
   {
      private static readonly IDictionary<string, Func<object>> _typeWithDefaultConstructorCache = new Dictionary<string, Func<object>>();
      private static readonly IDictionary<string, dynamic> _typeConstructorWithParameterCache = new Dictionary<string, dynamic>();

      public static object CreateInstance(Type type)
      {
         if (type == null)
         {
            throw new ArgumentNullException(nameof(type));
         }

         var typeConstructor = GetTypeWithDefaultConstructor(type);
         if (typeConstructor == default)
         {
            throw new InvalidOperationException($"Unable to create constructor for type '{type.FullName}'");
         }

         return typeConstructor();
      }

      private static Func<object> GetTypeWithDefaultConstructor(Type type)
      {
         if (_typeWithDefaultConstructorCache.TryGetValue(type.FullName, out var typeConstructor))
         {
            return typeConstructor;
         }
         // not already present, create it
         Helpers.Reflection.MakeGenericMethodAndInvoke(new CreateCreatorHelper(), "CreateCreatorWithDefaultConstructor", type, default);
         // should be present now
         return _typeWithDefaultConstructorCache[type.FullName];
      }

      public static object CreateInstance<TArg>(Type type, TArg constructorParameter)
      {
         if (type == null)
         {
            throw new ArgumentNullException(nameof(type));
         }

         var typeConstructor = GetTypeConstructorWithParameter<TArg>(type);
         if (typeConstructor == default)
         {
            throw new InvalidOperationException($"Unable to create constructor for type '{type.FullName}'");
         }

         return typeConstructor(constructorParameter);
      }

      private static Func<TArg, object> GetTypeConstructorWithParameter<TArg>(Type type)
      {
         if (_typeConstructorWithParameterCache.TryGetValue(type.FullName, out var typeConstructor))
         {
            if (typeConstructor is Func<TArg, object>)
            {
               return typeConstructor as Func<TArg, object>;
            }
         }

         // not already present, create it
         Helpers.Reflection.MakeGenericMethodAndInvoke(new CreateCreatorHelper(), "CreateCreatorWithConstructorParameter", new[] { typeof(TArg), type }, default);
         // should be present now
         return _typeConstructorWithParameterCache[type.FullName] as Func<TArg, object>;
      }

      private class CreateCreatorHelper
      {
#pragma warning disable IDE0051 // Remove unused private members, because in fact it's used, but using reflection
         private Func<T> CreateCreatorWithDefaultConstructor<T>()
#pragma warning restore IDE0051 // Remove unused private members
         {
            var newExpression = Expression.New(typeof(T));
            var creatorExpression = Expression.Lambda<Func<T>>(newExpression);

            var typeConstructor = creatorExpression.Compile();
            _typeWithDefaultConstructorCache.Add(typeof(T).FullName, typeConstructor as Func<object>);

            return typeConstructor;
         }

#pragma warning disable IDE0051 // Remove unused private members, because in fact it's used, but using reflection
         private Func<TArg, T> CreateCreatorWithConstructorParameter<TArg, T>()
#pragma warning restore IDE0051 // Remove unused private members
         {
            var constructor = typeof(T).GetConstructor(new Type[] { typeof(TArg) });
            var parameter = Expression.Parameter(typeof(TArg), "p");
            var newExpression = Expression.New(constructor, new Expression[] { parameter });

            var creatorExpression = Expression.Lambda<Func<TArg, T>>(newExpression, parameter);

            var typeConstructor = creatorExpression.Compile();
            _typeConstructorWithParameterCache.Add(typeof(T).FullName, typeConstructor);

            return typeConstructor;
         }
      }
   }
}
