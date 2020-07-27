using System;
using System.Threading.Tasks;
using Messages;
using NServiceBus;
using NServiceBus.Logging;
using Transaction.Services.Interfaces;

namespace Transaction.Messaging
{
    public class TransactionSagaHandler: Saga<TransactionSagaHandler.SagaData>,
                    IAmStartedByMessages<TransactionAdded>,
                    IHandleMessages<UpdateTransaction>
    {
        private readonly IUpdateTransactionService _service;
        static ILog log = LogManager.GetLogger<TransactionSagaHandler>();
        public TransactionSagaHandler(IUpdateTransactionService service)
        {
            _service = service;
        }

        public Task Handle(TransactionAdded message, IMessageHandlerContext context)
        {
            log.Info("transaction added");
            Data.TransactionId = message.TransactionId;
            return context.Send(new AddTransaction() {
                ToAccount=message.ToAccount,
                FromAccount=message.FromAccount,
                Amount=message.Amount,
                MessageId=message.MessageId
            });
        }

        public Task Handle(UpdateTransaction message, IMessageHandlerContext context)
        {
            log.Info("transaction updated");
            _service.UpdateTransaction(Data.TransactionId, message.Succeeded, message.Message);
            MarkAsComplete();
            return Task.CompletedTask;
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<SagaData> mapper)
        {
            mapper.ConfigureMapping<TransactionAdded>(message => message.MessageId)
                .ToSaga(sagaData => sagaData.SagaId);
            mapper.ConfigureMapping<UpdateTransaction>(message => message.MessageId)
                .ToSaga(sagaData => sagaData.SagaId);
        }

        public class SagaData : ContainSagaData
        {
            public string SagaId { get; set; }
            public Guid TransactionId { get; set; }
        } 

    }
}
