using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Qujck.Core.Events;

namespace Qujck.Core.Queries
{
    public sealed class QueryHandlerEventDecorator<TQuery, TResult> :
         IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        private readonly IQueryHandler<TQuery, TResult> decorated;
        private readonly IEnumerable<IEventHandler<OnBefore<TQuery>>> beforeEventHandlers;
        private readonly IEnumerable<IEventHandler<OnAfter<TQuery>>> afterEventHandlers;
        private readonly IEnumerable<IEventHandler<OnAfter<TQuery, TResult>>> afterEventHandlers2;

        public QueryHandlerEventDecorator(
            IQueryHandler<TQuery, TResult> decorated,
            IEnumerable<IEventHandler<OnBefore<TQuery>>> beforeEventHandlers,
            IEnumerable<IEventHandler<OnAfter<TQuery>>> afterEventHandlers,
            IEnumerable<IEventHandler<OnAfter<TQuery, TResult>>> afterEventHandlers2)
        {
            this.decorated = decorated;
            this.beforeEventHandlers = beforeEventHandlers;
            this.afterEventHandlers = afterEventHandlers;
            this.afterEventHandlers2 = afterEventHandlers2;
        }

        public TResult Handle(TQuery query)
        {
            var beforeEvent = new OnBefore<TQuery>(query);
            this.RunEvents(this.beforeEventHandlers, beforeEvent);

            var result = this.decorated.Handle(query);

            var afterEvent = new OnAfter<TQuery>(query);
            this.RunEvents(this.afterEventHandlers, afterEvent);
            var afterEvent2 = new OnAfter<TQuery, TResult>(query, result);
            this.RunEvents(this.afterEventHandlers2, afterEvent2);

            return result;
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
