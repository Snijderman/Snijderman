using System.ComponentModel;

namespace Snijderman.Common.Data.Paging
{
   public enum Operator
   {
      [Description("Equals")]
      Equals = 1,

      [Description("Not equals")]
      NotEquals = 2,

      [Description("Greater then")]
      Greater = 3,

      [Description("Less then")]
      Less = 4,

      [Description("Greater or equal")]
      GreaterEqual = 5,

      [Description("Less or equal")]
      LessEqual = 6,

      [Description("Contains")]
      Contains = 7,

      [Description("Does not contain")]
      NotContains = 8,

      [Description("In")]
      In = 9,

      [Description("Is Empty")]
      IsEmpty = 10
   }
}
