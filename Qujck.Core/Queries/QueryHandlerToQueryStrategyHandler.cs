namespace Qujck.Core.Queries
{
    public sealed class QueryHandlerToQueryStrategyHandler<TQuery, TResult> :
        IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        private readonly IQueryStrategyHandler<TQuery, TResult> queryStrategyHandler;

        public QueryHandlerToQueryStrategyHandler(
            IQueryStrategyHandler<TQuery, TResult> queryStrategyHandler)
        {
            this.queryStrategyHandler = queryStrategyHandler;
        }

        public TResult Handle(TQuery query)
        {
            return this.queryStrategyHandler.Handle(query);
        }
    }
}