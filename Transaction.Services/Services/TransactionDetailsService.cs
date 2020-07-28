using System;
using System.Threading.Tasks;
using Transaction.Services.Interfaces;

namespace Transaction.Services.Services
{
    public class TransactionDetailsService : ITransactionDetailsService
    {
        private readonly ITransactionDetailsRepository _repository;

        public TransactionDetailsService(ITransactionDetailsRepository repository)
        {
            _repository = repository;
        }

        public async Task<object> GetTransaction(Guid id)
        {
            return await _repository.GetTransaction(id);
        }
    }
}
