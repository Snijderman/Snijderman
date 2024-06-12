namespace Snijderman.Common.Mvvm;

public interface IMvvmControlService
{
   public IMvvmControl<TVm> GetControl<TVm>() where TVm : IMvvmViewModel;
}
