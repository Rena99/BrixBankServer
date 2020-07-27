using System;
using System.Threading.Tasks;

namespace Account.Services.Interfaces
{
    public interface IAddTransactionService
    {
        Task<string> AddTransaction(Guid fromAccount, Guid toAccount, int amount);
    }
}
