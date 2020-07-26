using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Messages;

namespace Account.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<int> AddTransaction(AddTransaction message);
    }
}
