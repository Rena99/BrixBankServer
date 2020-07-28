using System;

namespace Account.Services.Interfaces
{
    public interface IAddHistoryService
    {
        void AddHistory(Guid fromAccount, Guid toAccount, int amount, Guid transactionId);
    }
}
