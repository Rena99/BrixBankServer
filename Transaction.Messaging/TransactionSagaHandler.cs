using System;
using System.Threading.Tasks;
using Messages;
using NServiceBus;

namespace Transaction.Messaging
{
    public class TransactionSagaHandler: Saga<TransactionSagaHandler.SagaData>,
                    IAmStartedByMessages<TransactionAdded>,
                    IHandleMessages<UpdateTransaction>
        {
        public Task Handle(TransactionAdded message, IMessageHandlerContext context)
        {
            return context.Send(new AddTransaction() {
                ToAccount=message.ToAccount,
                FromAccount=message.FromAccount,
                Amount=message.Amount,
                MessageId=message.MessageId
            });
        }

        public Task Handle(UpdateTransaction message, IMessageHandlerContext context)
        {
            MarkAsComplete();
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<SagaData> mapper)
        {
            mapper.ConfigureMapping<TransactionAdded>(message => message.MessageId)
                .ToSaga(sagaData => sagaData.SagaId);
            mapper.ConfigureMapping<AddTransaction>(message => message.MessageId)
                .ToSaga(sagaData => sagaData.SagaId);
        }

        public class SagaData : ContainSagaData
        {
            public string SagaId { get; set; }
        }

    }
}
