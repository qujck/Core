namespace Qujck.Core.Commands
{
    public sealed class CommandHandlerToCommandStrategyHandler<TCommand> :
        ICommandHandler<TCommand> where TCommand : ICommand
    {
        private readonly ICommandStrategyHandler<TCommand> commandStrategyHandler;

        public CommandHandlerToCommandStrategyHandler(
            ICommandStrategyHandler<TCommand> commandStrategyHandler)
        {
            this.commandStrategyHandler = commandStrategyHandler;
        }

        public void Handle(TCommand command)
        {
            this.commandStrategyHandler.Handle(command);
        }
    }
}
