using System;
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

        public int AddTransaction(Guid fromAccount, Guid toAccount, int amount, out string errorMessage)
        {
            try
            {
                errorMessage = "";
                return _repository.AddTransaction(fromAccount, toAccount, amount).Result;
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
                return 2;
            }
        }
    }
}
