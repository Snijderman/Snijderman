using System;
using System.Collections;
using System.Windows.Markup;
using System.Xaml;

namespace Snijderman.Common.Wpf.Themes.Default.Helpers;

internal class ResourceAliasExtension
    : MarkupExtension
{
   public object ResourceKey { get; set; }

   public override object ProvideValue(IServiceProvider serviceProvider)
   {
      var rootObjectProvider = (IRootObjectProvider)serviceProvider.GetService(typeof(IRootObjectProvider));
      var dictionary = rootObjectProvider?.RootObject as IDictionary;
      return dictionary?[this.ResourceKey];
   }
}
