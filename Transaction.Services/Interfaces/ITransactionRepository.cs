using System;
using System.Threading.Tasks;
using Transaction.Services.Models;

namespace Transaction.Services.Interfaces
{
    public interface ITransactionRepository
    {
        Task<Guid> AddTransaction(TransactionModel transactionModel);
    }
}
