using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qujck.Core.Events
{
    public class OnAfter<TRequest> : IEvent
    {
        public OnAfter(TRequest request)
        {
            this.Request = request;
        }

        public TRequest Request { get; private set; }
    }

    public class OnAfter<TRequest, TResponse>
    {
        public OnAfter(TRequest request, TResponse response)
        {
            this.Request = request;
            this.Response = response;
        }

        public TRequest Request { get; private set; }
        public TResponse Response { get; private set; }
    }
}
