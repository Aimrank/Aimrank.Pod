using MediatR;

namespace Aimrank.CSGO.Application.Contracts
{
    public interface IQueryHandler<TQuery, TResult> : IRequestHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
    }
}