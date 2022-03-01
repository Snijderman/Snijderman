namespace Snijderman.Common.Mvvm;

public interface IMvvmControlService
{
   public IMvvmControl<VM> GetControl<VM>() where VM : IMvvmViewModel;
}
