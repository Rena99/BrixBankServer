using System.Threading.Tasks;
using Account.Services.Interfaces;
using Messages;
using NServiceBus;

namespace Account.Messaging
{
    public class TransactionHandler : IHandleMessages<AddTransaction>
    {
        private readonly IAddTransactionService _service;

        public TransactionHandler(IAddTransactionService service)
        {
            _service = service;
        }

        public Task Handle(AddTransaction message, IMessageHandlerContext context)
        {
            string succeeded = _service.AddTransaction(message.FromAccount, message.ToAccount, message.Amount).Result;
            return context.Reply(new UpdateTransaction()
            {
                MessageId = message.MessageId,
                Message = succeeded,
                Succeeded = succeeded.Length > 0 ? 2 : 1
            });
        }
    }
}