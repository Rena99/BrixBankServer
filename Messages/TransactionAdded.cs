using System;
using NServiceBus;

namespace Messages
{
    public class TransactionAdded:IEvent
    {
        public string MessageId { get; set; }
        public int Succeeded { get; set; }
        public string Message { get; set; }
    }
}
