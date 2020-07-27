using System;

namespace Account.Services.Interfaces
{
    public interface IAddTransactionService
    {
        int AddTransaction(Guid fromAccount, Guid toAccount, int amount, out string errorMessage);
    }
}
