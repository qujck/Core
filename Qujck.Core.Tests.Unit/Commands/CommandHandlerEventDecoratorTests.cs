using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Qujck.Core.Commands;
using Qujck.Core.Events;
using Xunit;

namespace Qujck.Core.Tests.Unit.Commands
{
    public class CommandHandlerEventDecoratorTests
    {
        [Fact]
        public void Handle_CallsAllBeforeEventHandlers_BeforeCallingDecoratedInstance()
        {
            bool called = false;
            bool decoratedInstanceHasBeenCalled = false;

            var beforeEventHandlers = new[]
            {
                new MockEventHandler<OnBefore<FakeCommand>>(parameter => decoratedInstanceHasBeenCalled = called),
                new MockEventHandler<OnBefore<FakeCommand>>(parameter => decoratedInstanceHasBeenCalled = called),
                new MockEventHandler<OnBefore<FakeCommand>>(parameter => decoratedInstanceHasBeenCalled = called)
            };

            var decoratedInstance = new MockCommandHandler<FakeCommand>(
                command => called = true);

            var decorator = this.DecoratorFactory(
                decoratedInstance,
                beforeEventHandlers,
                null);

            decorator.Handle(new FakeCommand());

            Assert.False(decoratedInstanceHasBeenCalled);
        }

        [Fact]
        public void Handle_CallsAllAfterEventHandlers_AfterCallingDecoratedInstance()
        {
            bool called = false;
            bool decoratedInstanceHasBeenCalled = false;

            var afterEventHandlers = new[]
            {
                new MockEventHandler<OnAfter<FakeCommand>>(parameter => decoratedInstanceHasBeenCalled = called),
                new MockEventHandler<OnAfter<FakeCommand>>(parameter => decoratedInstanceHasBeenCalled = called),
                new MockEventHandler<OnAfter<FakeCommand>>(parameter => decoratedInstanceHasBeenCalled = called)
            };

            var decoratedInstance = new MockCommandHandler<FakeCommand>(
                command => called = true);

            var decorator = this.DecoratorFactory(
                decoratedInstance,
                null,
                afterEventHandlers);

            decorator.Handle(new FakeCommand());

            Assert.True(decoratedInstanceHasBeenCalled);
        }

        [Fact]
        public void Handle_Always_PassesTheCommandIntoEachBeforeEventHandler()
        {
            var beforeEventHandlers = new[]
            {
                new MockEventHandler<OnBefore<FakeCommand>>(),
                new MockEventHandler<OnBefore<FakeCommand>>(),
                new MockEventHandler<OnBefore<FakeCommand>>()
            };

            var decorator = this.DecoratorFactory(
                null,
                beforeEventHandlers,
                null);

            var command = new FakeCommand();

            decorator.Handle(command);

            Assert.Same(command, beforeEventHandlers[0].Parameter.Request);
            Assert.Same(command, beforeEventHandlers[1].Parameter.Request);
            Assert.Same(command, beforeEventHandlers[2].Parameter.Request);
        }

        [Fact]
        public void Handle_Always_PassesTheCommandIntoEachAfterEventHandler()
        {
            var afterEventHandlers = new[]
            {
                new MockEventHandler<OnAfter<FakeCommand>>(),
                new MockEventHandler<OnAfter<FakeCommand>>(),
                new MockEventHandler<OnAfter<FakeCommand>>()
            };

            var decorator = this.DecoratorFactory(
                null,
                null,
                afterEventHandlers);

            var command = new FakeCommand();

            decorator.Handle(command);

            Assert.Same(command, afterEventHandlers[0].Parameter.Request);
            Assert.Same(command, afterEventHandlers[1].Parameter.Request);
            Assert.Same(command, afterEventHandlers[2].Parameter.Request);
        }

        private CommandHandlerEventDecorator<FakeCommand> DecoratorFactory(
            ICommandHandler<FakeCommand> decoratedInstance,
            IEnumerable<IEventHandler<OnBefore<FakeCommand>>> beforeEventHandlers,
            IEnumerable<IEventHandler<OnAfter<FakeCommand>>> afterEventHandlers)
        {
            return new CommandHandlerEventDecorator<FakeCommand>(
                decoratedInstance ?? new MockCommandHandler<FakeCommand>(),
                beforeEventHandlers ?? Enumerable.Empty<IEventHandler<OnBefore<FakeCommand>>>(),
                afterEventHandlers ?? Enumerable.Empty<IEventHandler<OnAfter<FakeCommand>>>());
        }

        private class FakeCommand : ICommand { }
    }
}
