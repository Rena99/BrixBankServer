using System;
using NServiceBus;

namespace Messages
{
    public class AddOperation : ICommand
    {
        public string MessageId { get; set; }
        public Guid FromAccount { get; set; }
        public Guid TransactionId { get; set; }
        public Guid ToAccount { get; set; }
        public int Amount { get; set; }
        public bool Succeeded { get; set; }
    }
}
