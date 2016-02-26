namespace Qujck.Core.Commands
{
    public interface ICommandDataHandler<TCommand>
    {
        void Handle(TCommand command);
    }
}
