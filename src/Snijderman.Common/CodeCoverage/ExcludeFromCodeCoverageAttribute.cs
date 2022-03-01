using System;

namespace Snijderman.Common.CodeCoverage;

[ExcludeFromCodeCoverage("Self exclusion.")]
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Event,
Inherited = false)]
public sealed class ExcludeFromCodeCoverageAttribute : Attribute
{
   public string Justification { get; }

   /// <summary>
   /// Exclude Code Coverage with justification
   /// </summary>
   /// <param name="justification">The reason why the code is excluded from codecoverage</param>
   public ExcludeFromCodeCoverageAttribute(string justification)
   {
      this.Justification = justification;
   }
}
