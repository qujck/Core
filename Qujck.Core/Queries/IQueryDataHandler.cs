namespace Qujck.Core.Queries
{
    public interface IQueryDataHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        TResult Handle(TQuery query);
    }
}
