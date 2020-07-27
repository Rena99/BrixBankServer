using System;
using System.Threading.Tasks;
using Messages;
using NServiceBus;
using Transaction.Services.Interfaces;

namespace Transaction.Messaging
{
    class UpdateTransactionHandler : IHandleMessages<UpdateTransaction>
    {
        private readonly IUpdateTransactionService _service;

        public UpdateTransactionHandler(IUpdateTransactionService service)
        {
            _service = service;
        }

        public Task Handle(UpdateTransaction message, IMessageHandlerContext context)
        {
            _service.UpdateTransaction(message.TransactionId, message.Succeeded, message.Message);
            return context.Publish(new TransactionUpdated()
            {
                MessageId=message.MessageId
            });
        }
    }
}
