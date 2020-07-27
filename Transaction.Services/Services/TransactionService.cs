using System;
using System.Threading.Tasks;
using Transaction.Services.Interfaces;
using Transaction.Services.Models;

namespace Transaction.Services.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _repository;

        public TransactionService(ITransactionRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> AddTransaction(TransactionModel transactionModel)
        {
            return await _repository.AddTransaction(transactionModel);
        }
    }
}
