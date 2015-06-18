using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qujck.Core.Events
{
    public interface IEventHandler<TEvent> where TEvent : IEvent
    {
        void Handle(TEvent parameter);
    }
}
