using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Qujck.Core.Queries;

namespace Qujck.Core.Tests.Unit
{
    internal class StubQueryHandler<TQuery, TResult> : 
        IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        public TQuery Query;
        public TResult Result;
        public Action Action;

        public StubQueryHandler() { }

        public StubQueryHandler(TResult result)
        {
            this.Result = result;
        }

        public StubQueryHandler(Action action, TResult result)
        {
            this.Action = action;
            this.Result = result;
        }

        public StubQueryHandler(Action action)
        {
            this.Action = action;
        }

        public TResult Handle(TQuery query)
        {
            this.Query = query;
            if (this.Action != null)
            {
                this.Action();
            }
            return this.Result;
        }
    }
}
