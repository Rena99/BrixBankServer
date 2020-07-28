using System;
using System.Threading.Tasks;
using Transaction.Services.Models;

namespace Transaction.Services.Interfaces
{
    public interface ITransactionDetailsRepository
    {
        Task<TransactionModel> GetTransaction(Guid id);
    }
}
