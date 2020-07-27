using System;
using System.Threading.Tasks;

namespace Account.Services.Interfaces
{
    public interface IAddTransactionRepository
    {
        Task<string> AddTransaction(Guid fromAccount, Guid toAccount, int amount);
    }
}
