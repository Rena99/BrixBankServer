using System.Threading.Tasks;
using Account.Services.Interfaces;
using Messages;
using NServiceBus;

namespace Account.Messaging
{
    public class TransactionHandler : IHandleMessages<AddTransaction>
    {
        private readonly ITransactionService _service;
        public TransactionHandler(ITransactionService service)
        {
            _service = service;
        }
        public async Task<Task> Handle(AddTransaction message, IMessageHandlerContext context)
        {
            int succeeded = await _service.AddTransaction(message);
            return context.Send(new UpdateTransaction()
            {
                MessageId = message.MessageId,
                Succeeded = succeeded
            });
        }
    }
}