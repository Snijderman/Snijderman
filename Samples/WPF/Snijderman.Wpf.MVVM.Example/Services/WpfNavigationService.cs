using System;
using Snijderman.Common.Mvvm;

namespace Snijderman.Wpf.MVVM.Example.Services;

public class WpfNavigationService : NavigationService
{
   public WpfNavigationService(IMvvmControlService mvvmControlService, IServiceProvider services) : base(mvvmControlService, services)
   {

   }
}
