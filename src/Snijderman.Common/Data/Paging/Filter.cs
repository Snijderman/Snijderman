namespace Snijderman.Common.Data.Paging;

public class Filter
{
   public string AndOr { get; set; }

   public string Operator { get; set; }

   public string Column { get; set; }

   public object Value { get; set; }
}
