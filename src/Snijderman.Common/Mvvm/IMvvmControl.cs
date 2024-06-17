namespace Snijderman.Common.Mvvm;

public interface IMvvmControl<out TVm> where TVm : IMvvmViewModel
{
   public TVm GetViewModel();
}
