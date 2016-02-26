namespace Qujck.Core.Queries
{
    interface IQueryStrategyHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        TResult Handle(TQuery query);
    }
}
