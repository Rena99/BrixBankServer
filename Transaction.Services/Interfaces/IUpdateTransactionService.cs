using System;

namespace Transaction.Services.Interfaces
{
    public interface IUpdateTransactionService
    {
        void UpdateTransaction(Guid transactionId, int succeeded, string message);
    }
}
