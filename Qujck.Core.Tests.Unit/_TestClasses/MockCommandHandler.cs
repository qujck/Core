using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Qujck.Core.Commands;

namespace Qujck.Core.Tests.Unit
{
    internal class MockCommandHandler<TCommand> : 
        ICommandHandler<TCommand> where TCommand : ICommand
    {
        public TCommand Command;
        public Action<TCommand> Action;

        public MockCommandHandler() { }

        public MockCommandHandler(Action<TCommand> action)
        {
            this.Action = action;
        }

        public void Handle(TCommand command)
        {
            this.Command = command;
            if (this.Action != null)
            {
                this.Action(command);
            }
        }
    }
}
