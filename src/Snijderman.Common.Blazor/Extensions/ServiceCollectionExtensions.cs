using Microsoft.Extensions.DependencyInjection;
using Snijderman.Common.Blazor.Internal.Bindings;
using Snijderman.Common.Blazor.Internal.Parameters;
using Snijderman.Common.Blazor.Internal.WeakEventListener;

namespace Snijderman.Common.Blazor.Extensions
{
   public static class ServiceCollectionExtensions
   {
      public static IServiceCollection AddBlazorMvvm(this IServiceCollection serviceCollection)
      {
         serviceCollection.AddSingleton<IWeakEventManagerFactory, WeakEventManagerFactory>();
         serviceCollection.AddSingleton<IBindingFactory, BindingFactory>();
         serviceCollection.AddSingleton<IParameterResolver, ParameterResolver>();
         serviceCollection.AddSingleton<IParameterCache, ParameterCache>();
         serviceCollection.AddSingleton<IViewModelParameterSetter, ViewModelParameterSetter>();
         //serviceCollection.AddHttpContextAccessor();

         return serviceCollection;
      }
   }
}
