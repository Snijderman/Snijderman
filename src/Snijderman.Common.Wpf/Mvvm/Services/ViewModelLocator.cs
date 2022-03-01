using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Snijderman.Common.Mvvm;

namespace Snijderman.Common.Wpf.Mvvm.Services;

public class ViewModelLocator : IViewModelLocator
{
   private readonly IServiceProvider _serviceProvider;

   public ViewModelLocator(IServiceProvider serviceProvider)
   {
      this._serviceProvider = serviceProvider;
   }

   public T GetPageViewModel<T>() where T : class, IMvvmViewModel
   {
      // zoek op meegegeven type
      var viewModelFromType = this._serviceProvider.GetService<T>();
      if (viewModelFromType != null)
      {
         return viewModelFromType;
      }

      // niet gelukt, nu nog door alle geregistreerde IMvvmViewModel om te zoeken of ie daar tussen zit
      var viewModelFromRegisterdInterfaces = this._serviceProvider.GetServices<IMvvmViewModel>().FirstOrDefault(x => x.GetType() == typeof(T));
      if (viewModelFromRegisterdInterfaces != null)
      {
         return viewModelFromRegisterdInterfaces as T;
      }

      return default;
   }
}
