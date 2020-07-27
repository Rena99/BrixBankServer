using System;
using System.Threading.Tasks;

namespace Account.Services.Interfaces
{
    public interface IAddTransactionRepository
    {
        Task<int> AddTransaction(Guid fromAccount, Guid toAccount, int amount);
    }
}
