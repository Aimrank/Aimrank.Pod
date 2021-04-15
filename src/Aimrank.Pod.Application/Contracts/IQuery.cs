using MediatR;

namespace Aimrank.Pod.Application.Contracts
{
    public interface IQuery<out TResult> : IRequest<TResult>
    {
    }
}