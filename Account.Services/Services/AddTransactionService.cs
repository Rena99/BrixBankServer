using System.Threading.Tasks;
using Account.Services.Interfaces;
using Messages;

namespace Account.Services.Services
{
    public class AddTransactionService : IAddTransactionService
    {
        private readonly IAddTransactionRepository _repository;

        public AddTransactionService(IAddTransactionRepository repository)
        {
            _repository = repository;
        }

        public async Task<UpdateTransaction> AddTransaction(AddTransaction message)
        {
            return await _repository.AddTransaction(message);
        }
    }
}
