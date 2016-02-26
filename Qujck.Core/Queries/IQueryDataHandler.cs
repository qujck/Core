namespace Qujck.Core.Queries
{
    interface IQueryDataHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        TResult Handle(TQuery query);
    }
}
