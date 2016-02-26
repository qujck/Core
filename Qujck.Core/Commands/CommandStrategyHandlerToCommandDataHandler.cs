namespace Qujck.Core.Commands
{
    public sealed class CommandStrategyHandlerToCommandDataHandler<TCommand> :
        ICommandStrategyHandler<TCommand> where TCommand : ICommand
    {
        private readonly ICommandDataHandler<TCommand> commandDataHandler;

        public CommandStrategyHandlerToCommandDataHandler(
            ICommandDataHandler<TCommand> commandDataHandler)
        {
            this.commandDataHandler = commandDataHandler;
        }

        public void Handle(TCommand command)
        {
            this.commandDataHandler.Handle(command);
        }
    }
}
