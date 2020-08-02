using System.Threading.Tasks;
using Account.Services.Interfaces;
using Messages;
using NServiceBus;

namespace Account.Messaging
{
    public class HistoryHandler : IHandleMessages<AddOperation>
    {
        private readonly IAddHistoryService _historyService;

        public HistoryHandler(IAddHistoryService historyService)
        {
            _historyService = historyService;
        }

        public Task Handle(AddOperation message, IMessageHandlerContext context)
        {
            _historyService.AddHistory(message.FromAccount, message.ToAccount, message.Amount, message.TransactionId, message.Succeeded);
            return Task.CompletedTask;
        }
    }
}
