namespace Snijderman.Common.Blazor.Internal.WeakEventListener;

internal interface IWeakEventManagerFactory
{
   IWeakEventManager Create();
}

internal class WeakEventManagerFactory : IWeakEventManagerFactory
{
   public IWeakEventManager Create() => new WeakEventManager();
}
