using NServiceBus;
using System;

namespace Messages
{
    public class AddTransaction : ICommand
    {
        public string MessageId { get; set; }
        public Guid FromAccount { get; set; }
        public Guid ToAccount { get; set; }
        public int Amount { get; set; }
    }
}
