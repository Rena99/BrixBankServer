using System;

namespace Account.Services.Interfaces
{
    public interface IAddHistoryRepository
    {
        void AddHistory(Guid fromAccount, Guid toAccount, int amount, Guid transactionId);
    }
}
