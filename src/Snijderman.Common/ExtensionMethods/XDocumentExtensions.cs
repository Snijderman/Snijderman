using System;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Snijderman.Common.ExtensionMethods;

public static class XNodeExtensions
{
   public static XElement SelectElement(this XNode result, string pathToElement, XNamespace defaultNamespace) => SelectElement(result, pathToElement, defaultNamespace?.ToString());

   public static XElement SelectElement(this XNode result, string pathToElement, string defaultNamespace)
   {
      if (pathToElement == null)
      {
         throw new ArgumentNullException(nameof(pathToElement));
      }

      var nsManager = new XmlNamespaceManager(new NameTable());
      nsManager.AddNamespace("def", defaultNamespace);
      var fixedPathToElement = pathToElement.Replace("/", "/def:", StringComparison.OrdinalIgnoreCase);
      return result.XPathSelectElement(fixedPathToElement, nsManager);
   }
}
