using System;
using Account.Services.Interfaces;

namespace Account.Services.Services
{
    public class AddHistoryService : IAddHistoryService
    {
        private readonly IAddHistoryRepository _repository;

        public AddHistoryService(IAddHistoryRepository repository)
        {
            _repository = repository;
        }
        public void AddHistory(Guid fromAccount, Guid toAccount, int amount, Guid transactionId)
        {
            _repository.AddHistory(fromAccount, toAccount, amount, transactionId);
        }
    }
}
