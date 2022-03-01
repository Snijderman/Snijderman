using MediatR;

namespace Snijderman.Common.MediatR;

public interface IQuery<out TResult> : IRequest<TResult>
{

}
