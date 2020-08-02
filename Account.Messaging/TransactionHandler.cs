using System.Threading.Tasks;
using Account.Services.Interfaces;
using Messages;
using NServiceBus;

namespace Account.Messaging
{
    public class TransactionHandler : IHandleMessages<AddTransaction>
    {
        private readonly IAddTransactionService _transactionService;

        public TransactionHandler(IAddTransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        public Task Handle(AddTransaction message, IMessageHandlerContext context)
        {
            string errorMessage;
            int succeeded = _transactionService.AddTransaction(message.FromAccount, message.ToAccount, message.Amount, out errorMessage);
            _ = context.SendLocal(new AddOperation()
            {
                MessageId = message.MessageId,
                Amount = message.Amount,
                FromAccount = message.FromAccount,
                ToAccount = message.ToAccount,
                TransactionId = message.TransactionId,
                Succeeded = succeeded == 1?true:false
            });
            return context.Publish(new TransactionAdded()
            {
                MessageId = message.MessageId,
                Message = errorMessage,
                Succeeded = succeeded
            });
        }
    }
}