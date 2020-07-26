using NServiceBus;

namespace Messages
{
    public class UpdateTransaction:ICommand
    {
        public string MessageId { get; set; }
        public int Succeeded { get; set; }
    }
}
