using System;
using Microsoft.AspNetCore.Components;
using Snijderman.Common.Mvvm;

namespace Snijderman.Common.Blazor.Internal.Parameters
{
   internal interface IViewModelParameterSetter
   {
      void ResolveAndSet(ComponentBase component, ViewModelBase viewModel);
   }

   internal class ViewModelParameterSetter : IViewModelParameterSetter
   {
      private readonly IParameterCache _parameterCache;
      private readonly IParameterResolver _parameterResolver;

      public ViewModelParameterSetter(IParameterResolver parameterResolver, IParameterCache parameterCache)
      {
         this._parameterResolver = parameterResolver ?? throw new ArgumentNullException(nameof(parameterResolver));
         this._parameterCache = parameterCache ?? throw new ArgumentNullException(nameof(parameterCache));
      }

      public void ResolveAndSet(ComponentBase component, ViewModelBase viewModel)
      {
         if (component == null)
         {
            throw new ArgumentNullException(nameof(component));
         }

         if (viewModel == null)
         {
            throw new ArgumentNullException(nameof(viewModel));
         }

         var componentType = component.GetType();

         var parameterInfo = this._parameterCache.Get(componentType);
         if (parameterInfo == null)
         {
            var componentParameters = this._parameterResolver.ResolveParameters(componentType);
            var viewModelParameters = this._parameterResolver.ResolveParameters(viewModel.GetType());
            parameterInfo = new ParameterInfo(componentParameters, viewModelParameters);
            this._parameterCache.Set(componentType, parameterInfo);
         }

         foreach ((var componentProperty, var viewModelProperty) in parameterInfo.Parameters)
         {
            var value = componentProperty.GetValue(component);
            viewModelProperty.SetValue(viewModel, value);
         }
      }
   }
}
