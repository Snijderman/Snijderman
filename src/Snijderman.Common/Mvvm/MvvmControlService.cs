using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace Snijderman.Common.Mvvm
{
   public class MvvmControlService : IMvvmControlService
   {
      protected static readonly Dictionary<Type, Type> _viewModelControls = new();
      protected readonly IServiceProvider _serviceProvider;

      public MvvmControlService(IServiceProvider serviceProvider)
      {
         this._serviceProvider = serviceProvider;
      }


      internal static void AddViewModelWithControl<VM, V>() where VM : IMvvmViewModel
                                                            where V : IMvvmControl<VM> => _viewModelControls.Add(typeof(VM), typeof(V));

      public virtual IMvvmControl<VM> GetControl<VM>() where VM : IMvvmViewModel
      {
         var controlType = this.GetControlForViewModel<VM>();
         return this._serviceProvider.GetRequiredService(controlType) as IMvvmControl<VM>;
      }

      private Type GetControlForViewModel<VM>() where VM : IMvvmViewModel
      {
         if (!_viewModelControls.TryGetValue(typeof(VM), out var controlType))
         {
            throw new ArgumentException($"Control not found for viewmodel type '{typeof(VM).FullName}'");
         }

         return controlType;
      }
   }
}
