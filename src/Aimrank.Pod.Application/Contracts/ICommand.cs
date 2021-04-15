using MediatR;

namespace Aimrank.Pod.Application.Contracts
{
    public interface ICommand : ICommand<Unit>
    {
    }

    public interface ICommand<out TResult> : IRequest<TResult>
    {
    }
}