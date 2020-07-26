using System.Threading.Tasks;
using Account.Services.Interfaces;
using Messages;

namespace Account.Data.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        public async Task<int> AddTransaction(AddTransaction message)
        {
            throw new System.NotImplementedException();
        }
    }
}
