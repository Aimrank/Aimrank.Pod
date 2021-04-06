using MediatR;

namespace Aimrank.CSGO.Application.Contracts
{
    public interface IQuery<out TResult> : IRequest<TResult>
    {
    }
}