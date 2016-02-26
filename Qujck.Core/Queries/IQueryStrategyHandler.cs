namespace Qujck.Core.Queries
{
    public interface IQueryStrategyHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        TResult Handle(TQuery query);
    }
}
