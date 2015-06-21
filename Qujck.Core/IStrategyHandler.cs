namespace Qujck.Core
{
    public interface IStrategyHandler<TCommand> where TCommand : ICommandStrategy
    {
        void Handle(TCommand command);
    }

    public interface IStrategyHandler<TQuery, TResult> where TQuery : IQueryStrategy<TResult>
    {
        TResult Handle(TQuery query);
    }
}
