using System;
using Microsoft.Extensions.DependencyInjection;
using Snijderman.Common.Mvvm;

namespace Snijderman.Samples.Blazor.Mvvm.ViewModels;

public static class ExtensionMethods
{
   public static IServiceCollection AddMvvmViewModels(this IServiceCollection services)
   {
      services.AddTransient<ClockViewModel>();
      services.AddTransient<DashboardViewModel>();
      services.AddTransient<CustomerViewModel>();
      services.AddTransient<OrderViewModel>();
      services.AddTransient<OrderDetailViewModel>();

      return services;
   }

   internal static TViewModel CreateAndFillViewModelProperties<TViewModel, TModel>(this IServiceProvider serviceProvider, TModel model, Action<TViewModel, TModel> fillPropertiesAction) where TViewModel : ViewModelBase
   {
      var viewModel = serviceProvider.GetRequiredService<TViewModel>();
      fillPropertiesAction(viewModel, model);
      return viewModel;
   }
}
