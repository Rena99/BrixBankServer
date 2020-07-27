using NServiceBus;

namespace Messages
{
    public class TransactionUpdated:IEvent
    {
        public string MessageId { get; set; }
    }
}
