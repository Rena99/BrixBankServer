using System.Threading.Tasks;
using Account.Services.Interfaces;
using Messages;
using NServiceBus;

namespace Account.Messaging
{
    public class TransactionHandler : IHandleMessages<AddTransaction>
    {
        private readonly IAddTransactionService _transactionService;
        private readonly IAddHistoryService _historyService;

        public TransactionHandler(IAddTransactionService transactionService, IAddHistoryService historyService)
        {
            _transactionService = transactionService;
            _historyService = historyService;
        }

        public Task Handle(AddTransaction message, IMessageHandlerContext context)
        {
            string errorMessage;
            int succeeded = _transactionService.AddTransaction(message.FromAccount, message.ToAccount, message.Amount, out errorMessage);
            if (succeeded == 1)
            {
                _historyService.AddHistory(message.FromAccount, message.ToAccount, message.Amount, message.TransactionId);
            }
            return context.Publish(new TransactionAdded()
            {
                MessageId = message.MessageId,
                Message = errorMessage,
                Succeeded = succeeded
            });
        }
    }
}