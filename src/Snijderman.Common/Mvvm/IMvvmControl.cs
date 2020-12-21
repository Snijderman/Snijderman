namespace Snijderman.Common.Mvvm
{
   public interface IMvvmControl<VM> where VM : IMvvmViewModel
   {
      public VM GetViewModel();
   }
}
