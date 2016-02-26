namespace Qujck.Core.Commands
{
    public interface ICommandStrategyHandler<TCommand>
    {
        void Handle(TCommand command);
    }
}
