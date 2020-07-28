using System;
using System.Threading.Tasks;

namespace Transaction.Services.Interfaces
{
    public interface ITransactionDetailsService
    {
        Task<object> GetTransaction(Guid id);
    }
}
