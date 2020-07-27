using System;

namespace Transaction.Services.Interfaces
{
    public interface IUpdateTransactionRepository
    {
        void UpdateTransaction(Guid transactionId, int succeeded, string message);
    }
}
