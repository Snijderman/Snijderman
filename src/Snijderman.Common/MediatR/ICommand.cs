using MediatR;

namespace Snijderman.Common.MediatR;

public interface ICommand : IRequest
{
}

public interface ICommand<out TResult> : IRequest<TResult>
{
}
