using System.Threading.Tasks;
using Messages;

namespace Account.Services.Interfaces
{
    public interface IAddTransactionRepository
    {
        Task<UpdateTransaction> AddTransaction(AddTransaction message);
    }
}
