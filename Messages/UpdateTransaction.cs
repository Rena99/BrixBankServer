using System;
using NServiceBus;

namespace Messages
{
    public class UpdateTransaction:ICommand
    {
        public string MessageId { get; set; }
        public Guid TransactionId { get; set; }
        public int Succeeded { get; set; }
        public string Message { get; set; }
    }
}
