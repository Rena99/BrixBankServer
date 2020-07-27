using System;
using System.Threading.Tasks;
using Account.Services.Interfaces;

namespace Account.Services.Services
{
    public class AddTransactionService : IAddTransactionService
    {
        private readonly IAddTransactionRepository _repository;

        public AddTransactionService(IAddTransactionRepository repository)
        {
            _repository = repository;
        }

        public async Task<string> AddTransaction(Guid fromAccount, Guid toAccount, int amount)
        {
            return await _repository.AddTransaction(fromAccount, toAccount, amount);
        }
    }
}
