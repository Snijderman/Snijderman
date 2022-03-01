using System.Collections.Generic;

namespace Snijderman.Common.Data.Paging;

public class MetaDataDefinitionPaging
{
   public string Name { get; set; }

   public IList<MetaDataPropertyDefinition> PropertyDefinitions { get; set; }
}
