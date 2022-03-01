using Snijderman.Common.CodeCoverage;

namespace Snijderman.Common.Helpers;

[ExcludeFromCodeCoverage("Technical helper")]
public static class ReferencedAssemblyHelper
{
   public static void EnsureAssemblyIsIncludedInOutput<T>()
   {
      // left empty on purpose, only needed to make sure referenced assembly is copied to output folder
   }
}
