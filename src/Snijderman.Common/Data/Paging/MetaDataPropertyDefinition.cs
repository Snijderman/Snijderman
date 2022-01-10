namespace Snijderman.Common.Data.Paging
{
   public class MetaDataPropertyDefinition
   {
      public string Name { get; set; }

      public string SqlName { get; set; }

      public MetaDataType MetaDataType { get; set; }
   }

   /// <summary>
   /// Makes string CamelCase.
   /// </summary>
   public static class PropertyExtensions
   {
      public static string ToCamelCase(this string name) => string.IsNullOrWhiteSpace(name) ? name : name.Substring(0, 1).ToLowerInvariant() + name[1..];
   }

}
