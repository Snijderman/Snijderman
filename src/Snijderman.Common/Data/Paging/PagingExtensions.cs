using System;

namespace Snijderman.Common.Data.Paging
{
   public static class PagingExtensions
   {
      public static Info FixInvalidSortDirection(this Info info)
      {
         if (string.IsNullOrWhiteSpace(info?.SortDirection))
         {
            return info;
         }

         if (info.SortDirection.StartsWith("asc", StringComparison.OrdinalIgnoreCase))
         {
            info.SortDirection = "asc";
         }
         else if (info.SortDirection.StartsWith("desc", StringComparison.OrdinalIgnoreCase))

         {
            info.SortDirection = "desc";
         }

         return info;
      }
   }
}
