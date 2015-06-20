using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Qujck.Core.Events;

namespace Qujck.Core.Commands
{
    public sealed class CommandHandlerEventDecorator<TCommand> :
        ICommandHandler<TCommand> where TCommand : ICommand
    {
        private readonly ICommandHandler<TCommand> decorated;
        private readonly IEnumerable<IEventHandler<OnBefore<TCommand>>> beforeEventHandlers;
        private readonly IEnumerable<IEventHandler<OnAfter<TCommand>>> afterEventHandlers;

        public CommandHandlerEventDecorator(
            ICommandHandler<TCommand> decorated,
            IEnumerable<IEventHandler<OnBefore<TCommand>>> beforeEventHandlers,
            IEnumerable<IEventHandler<OnAfter<TCommand>>> afterEventHandlers)
        {
            this.decorated = decorated;
            this.beforeEventHandlers = beforeEventHandlers;
            this.afterEventHandlers = afterEventHandlers;
        }

        public void Handle(TCommand command)
        {
            var beforeEvent = new OnBefore<TCommand>(command);
            this.RunEvents(this.beforeEventHandlers, beforeEvent);

            this.decorated.Handle(command);

            var afterEvent = new OnAfter<TCommand>(command);
            this.RunEvents(this.afterEventHandlers, afterEvent);
        }

        private void RunEvents<TEvent>(
            IEnumerable<IEventHandler<TEvent>> eventHandlers,
            TEvent parameter) where TEvent : IEvent
        {
            foreach (var handler in eventHandlers)
            {
                handler.Handle(parameter);
            }
        }
    }
}