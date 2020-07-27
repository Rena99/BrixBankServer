using System.Threading.Tasks;
using Account.Services.Interfaces;
using Messages;
using NServiceBus;
using NServiceBus.Logging;

namespace Account.Messaging
{
    public class TransactionHandler : IHandleMessages<AddTransaction>
    {
        private readonly IAddTransactionService _service;
        static ILog log = LogManager.GetLogger<TransactionHandler>();

        public TransactionHandler(IAddTransactionService service)
        {
            _service = service;
        }

        public Task Handle(AddTransaction message, IMessageHandlerContext context)
        {
            log.Info("transaction added");
            UpdateTransaction succeeded = _service.AddTransaction(message).Result;
            succeeded.MessageId = message.MessageId;
            return context.Send(succeeded);
        }
    }
}