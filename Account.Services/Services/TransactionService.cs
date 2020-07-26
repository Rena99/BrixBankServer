using System;
using System.Security.Principal;
using System.Threading.Tasks;
using Account.Services.Interfaces;
using Messages;

namespace Account.Services.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _repository;
        public TransactionService(ITransactionRepository repository)
        {
            _repository = repository;
        }
        public async Task<int> AddTransaction(AddTransaction message)
        {
            return await _repository.AddTransaction(message);
        }
    }
}
