using System.Collections.Generic;
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
            //use out parameter to get string and return status
            string errorMessage;
            int succeeded = _service.AddTransaction(message.FromAccount, message.ToAccount, message.Amount, out errorMessage);
            return context.Publish(new TransactionAdded()
            {
                MessageId = message.MessageId,
                Message = errorMessage,
                Succeeded = succeeded
            });
        }
    }
}