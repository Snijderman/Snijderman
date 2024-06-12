using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace Snijderman.Common.Mvvm;

public class MvvmControlService : IMvvmControlService
{
   protected static readonly Dictionary<Type, Type> ViewModelControls = new();
   protected readonly IServiceProvider ServiceProvider;

   public MvvmControlService(IServiceProvider serviceProvider)
   {
      this.ServiceProvider = serviceProvider;
   }


   internal static void AddViewModelWithControl<TVm, TV>() where TVm : IMvvmViewModel
                                                         where TV : IMvvmControl<TVm> => ViewModelControls.Add(typeof(TVm), typeof(TV));

   public virtual IMvvmControl<TVm> GetControl<TVm>() where TVm : IMvvmViewModel
   {
      var controlType = this.GetControlForViewModel<TVm>();
      return this.ServiceProvider.GetRequiredService(controlType) as IMvvmControl<TVm>;
   }

   private Type GetControlForViewModel<TVm>() where TVm : IMvvmViewModel
   {
      if (!ViewModelControls.TryGetValue(typeof(TVm), out var controlType))
      {
         throw new ArgumentException($"Control not found for viewmodel type '{typeof(TVm).FullName}'");
      }

      return controlType;
   }
}
