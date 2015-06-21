namespace Qujck.Core.Events
{
    public class OnBefore<TRequest> : IEvent
    {
        public OnBefore(TRequest request)
        {
            this.Request = request;
        }

        public TRequest Request { get; private set; }
    }
}
