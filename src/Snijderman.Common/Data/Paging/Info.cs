using System.Collections.Generic;

namespace Snijderman.Common.Data.Paging;

public class Info
{
   public int PageIndex { get; set; }

   public int PageSize { get; set; }

   public string SortName { get; set; }

   public string SortDirection { get; set; }

   public IList<Filter> Filters { get; set; }
}
