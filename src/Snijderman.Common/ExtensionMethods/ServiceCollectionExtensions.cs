using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Snijderman.Common.ExtensionMethods;

public static class ServiceCollectionExtensions
{
   /// <summary>
   /// Removes all registered <see cref="ServiceLifetime.Transient"/> registrations of <see cref="TService"/> and adds in <see cref="TImplementation"/>.
   /// </summary>
   /// <typeparam name="TService">The type of service interface which needs to be placed.</typeparam>
   /// <typeparam name="TImplementation">The test or mock implementation of <see cref="TService"/> to add into <see cref="services"/>.</typeparam>
   /// <param name="services"></param>
   public static void SwapTransient<TService, TImplementation>(this IServiceCollection services) where TImplementation : class, TService
   {
      if (services is null)
      {
         throw new ArgumentNullException(nameof(services));
      }

      RemoveRegisteredServiceTypes<TService>(services, ServiceLifetime.Transient);

      services.AddTransient(typeof(TService), typeof(TImplementation));
   }

   private static void RemoveRegisteredServiceTypes<TService>(IServiceCollection services, ServiceLifetime serviceLifetime)
   {
      if (services.Any(x => x.ServiceType == typeof(TService) && x.Lifetime == serviceLifetime))
      {
         var serviceDescriptors = services.Where(x => x.ServiceType == typeof(TService) && x.Lifetime == serviceLifetime).ToList();
         foreach (var serviceDescriptor in serviceDescriptors)
         {
            services.Remove(serviceDescriptor);
         }
      }
   }

   /// <summary>
   /// Removes all registered <see cref="ServiceLifetime.Transient"/> registrations of <see cref="TService"/> and adds a new registration which uses the <see cref="Func{IServiceProvider, TService}"/>.
   /// </summary>
   /// <typeparam name="TService">The type of service interface which needs to be placed.</typeparam>
   /// <param name="services"></param>
   /// <param name="implementationFactory">The implementation factory for the specified type.</param>
   public static void SwapTransient<TService>(this IServiceCollection services, Func<IServiceProvider, TService> implementationFactory)
   {
      if (services is null)
      {
         throw new ArgumentNullException(nameof(services));
      }

      RemoveRegisteredServiceTypes<TService>(services, ServiceLifetime.Transient);

      services.AddTransient(typeof(TService), sp => implementationFactory(sp));
   }

   /// <summary>
   /// Removes all registered <see cref="ServiceLifetime.Scoped"/> registrations of <see cref="TService"/> and adds in <see cref="TImplementation"/>.
   /// </summary>
   /// <typeparam name="TService">The type of service interface which needs to be placed.</typeparam>
   /// <typeparam name="TImplementation">The test or mock implementation of <see cref="TService"/> to add into <see cref="services"/>.</typeparam>
   /// <param name="services"></param>
   public static void SwapScoped<TService, TImplementation>(this IServiceCollection services) where TImplementation : class, TService
   {
      if (services is null)
      {
         throw new ArgumentNullException(nameof(services));
      }

      RemoveRegisteredServiceTypes<TService>(services, ServiceLifetime.Scoped);

      services.AddScoped(typeof(TService), typeof(TImplementation));
   }

   /// <summary>
   /// Removes all registered <see cref="ServiceLifetime.Scoped"/> registrations of <see cref="TService"/> and adds a new registration which uses the <see cref="Func{IServiceProvider, TService}"/>.
   /// </summary>
   /// <typeparam name="TService">The type of service interface which needs to be placed.</typeparam>
   /// <param name="services"></param>
   /// <param name="implementationFactory">The implementation factory for the specified type.</param>
   public static void SwapScoped<TService>(this IServiceCollection services, Func<IServiceProvider, TService> implementationFactory)
   {
      if (services is null)
      {
         throw new ArgumentNullException(nameof(services));
      }

      RemoveRegisteredServiceTypes<TService>(services, ServiceLifetime.Scoped);

      services.AddScoped(typeof(TService), sp => implementationFactory(sp));
   }

   /// <summary>
   /// Removes all registered <see cref="ServiceLifetime.Singleton"/> registrations of <see cref="TService"/> and adds in <see cref="TImplementation"/>.
   /// </summary>
   /// <typeparam name="TService">The type of service interface which needs to be placed.</typeparam>
   /// <typeparam name="TImplementation">The test or mock implementation of <see cref="TService"/> to add into <see cref="services"/>.</typeparam>
   /// <param name="services"></param>
   public static void SwapSingleton<TService, TImplementation>(this IServiceCollection services) where TImplementation : class, TService
   {
      if (services is null)
      {
         throw new ArgumentNullException(nameof(services));
      }

      RemoveRegisteredServiceTypes<TService>(services, ServiceLifetime.Singleton);

      services.AddSingleton(typeof(TService), typeof(TImplementation));
   }

   /// <summary>
   /// Removes all registered <see cref="ServiceLifetime.Singleton"/> registrations of <see cref="TService"/> and adds a new registration which uses the <see cref="Func{IServiceProvider, TService}"/>.
   /// </summary>
   /// <typeparam name="TService">The type of service interface which needs to be placed.</typeparam>
   /// <param name="services"></param>
   /// <param name="implementationFactory">The implementation factory for the specified type.</param>
   public static void SwapSingleton<TService>(this IServiceCollection services, Func<IServiceProvider, TService> implementationFactory)
   {
      if (services is null)
      {
         throw new ArgumentNullException(nameof(services));
      }

      RemoveRegisteredServiceTypes<TService>(services, ServiceLifetime.Singleton);

      services.AddSingleton(typeof(TService), sp => implementationFactory(sp));
   }

   public static TService TryGetServiceFromServiceCollection<TService>(this IServiceCollection services)
   {
      try
      {
         using var provider = services.BuildServiceProvider();
         return provider.GetService<TService>();
      }
      catch
      {
         return default;
      }
   }

   private static void AddServiceRegistration(this IServiceCollection services, Type t, ServiceLifetime lifetime, bool additionalRegisterTypesByThemself, TypeInfo type)
   {
      services.Add(new ServiceDescriptor(t, type, lifetime));

      if (additionalRegisterTypesByThemself)
      {
         services.Add(new ServiceDescriptor(type, type, lifetime));
      }
   }

   public static void AddAllTypesImplementingInterface<T>(this IServiceCollection services,
                                                          Assembly[] assemblies,
                                                          ServiceLifetime lifetime = ServiceLifetime.Transient,
                                                          bool addAbstract = false,
                                                          bool additionalRegisterTypesByThemself = false)
   {
      if (services is null)
      {
         throw new ArgumentNullException(nameof(services));
      }

      if (assemblies is null)
      {
         throw new ArgumentNullException(nameof(assemblies));
      }

      var typesFromAssemblies = assemblies.SelectMany(a => a.DefinedTypes.Where(x => x.GetInterfaces().Any(i => i == typeof(T))));

      foreach (var type in typesFromAssemblies)
      {
         if (type.IsAbstract && !addAbstract)
         {
            continue;
         }

         services.AddServiceRegistration(typeof(T), lifetime, additionalRegisterTypesByThemself, type);
      }
   }

   /// <summary>
   /// Add all generic implementations of a generic interface
   /// Baseclasses are 
   /// </summary>
   /// <param name="services"></param>
   /// <param name="t"></param>
   /// <param name="assemblies"></param>
   /// <param name="lifetime"></param>
   /// <param name="additionalRegisterTypesByThemself"></param>
   public static void AddAllGenericTypesImplementingInterface(this IServiceCollection services,
                                                              Type t,
                                                              Assembly[] assemblies,
                                                              ServiceLifetime lifetime = ServiceLifetime.Transient,
                                                              bool addAbstract = false,
                                                              bool additionalRegisterTypesByThemself = false)
   {
      if (services is null)
      {
         throw new ArgumentNullException(nameof(services));
      }

      if (assemblies is null)
      {
         throw new ArgumentNullException(nameof(assemblies));
      }

      var genericType = t;
      var typesFromAssemblies = assemblies.SelectMany(a => a.DefinedTypes.Where(x => x.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == genericType)));

      foreach (var type in typesFromAssemblies)
      {
         if (type.IsAbstract && !addAbstract)
         {
            continue;
         }

         services.AddServiceRegistration(t, lifetime, additionalRegisterTypesByThemself, type);
      }
   }

   public static void RegisterAllAssignableType<T>(this IServiceCollection services,
                                                   Assembly[] assemblies,
                                                   ServiceLifetime lifetime = ServiceLifetime.Transient,
                                                   bool addAbstract = false,
                                                   bool additionalRegisterTypesByThemself = false)
   {
      if (services is null)
      {
         throw new ArgumentNullException(nameof(services));
      }

      if (assemblies is null)
      {
         throw new ArgumentNullException(nameof(assemblies));
      }

      var typesFromAssemblies = assemblies.SelectMany(a => a.DefinedTypes).Where(p => typeof(T).IsAssignableFrom(p) && p.IsClass);

      foreach (var type in typesFromAssemblies)
      {
         if (type.IsAbstract && !addAbstract)
         {
            continue;
         }

         services.AddServiceRegistration(typeof(T), lifetime, additionalRegisterTypesByThemself, type);
      }
   }
}
