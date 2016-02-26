namespace Qujck.Core.Queries
{
    public sealed class QueryStrategyHandlerToQueryDataHandler<TQuery, TResult> :
        IQueryStrategyHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        private readonly IQueryDataHandler<TQuery, TResult> queryDataHandler;

        public QueryStrategyHandlerToQueryDataHandler(
            IQueryDataHandler<TQuery, TResult> queryDataHandler)
        {
            this.queryDataHandler = queryDataHandler;
        }

        public TResult Handle(TQuery query)
        {
            return this.queryDataHandler.Handle(query);
        }
    }
}
