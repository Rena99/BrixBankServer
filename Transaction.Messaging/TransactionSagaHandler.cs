using System;
using System.Threading.Tasks;
using Messages;
using NServiceBus;

namespace Transaction.Messaging
{
    public class TransactionSagaHandler: Saga<TransactionSagaHandler.SagaData>,
                    IAmStartedByMessages<TransactionReceived>,
                    IHandleMessages<TransactionAdded>,
                    IHandleMessages<TransactionUpdated>
    {
        public Task Handle(TransactionReceived message, IMessageHandlerContext context)
        {
            Data.TransactionId = message.TransactionId;
            return context.Send(new AddTransaction() {
                ToAccount=message.ToAccount,
                FromAccount=message.FromAccount,
                Amount=message.Amount,
                MessageId=message.MessageId,
                TransactionId=message.TransactionId
            });
        }

        public Task Handle(TransactionAdded message, IMessageHandlerContext context)
        {
            return context.SendLocal(new UpdateTransaction()
            {
                MessageId = message.MessageId,
                Succeeded = message.Succeeded,
                Message = message.Message,
                TransactionId=Data.TransactionId
            });
        }

        public Task Handle(TransactionUpdated message, IMessageHandlerContext context)
        {
            MarkAsComplete();
            return Task.CompletedTask;
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<SagaData> mapper)
        {
            mapper.ConfigureMapping<TransactionAdded>(message => message.MessageId)
                .ToSaga(sagaData => sagaData.SagaId);
            mapper.ConfigureMapping<TransactionReceived>(message => message.MessageId)
                .ToSaga(sagaData => sagaData.SagaId);
            mapper.ConfigureMapping<TransactionUpdated>(message => message.MessageId)
               .ToSaga(sagaData => sagaData.SagaId);
        }

        public class SagaData : ContainSagaData
        {
            public string SagaId { get; set; }
            public Guid TransactionId { get; set; }
        } 

    }
}
