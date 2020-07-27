using System;
using Transaction.Services.Interfaces;

namespace Transaction.Services.Services
{
    public class UpdateTransactionService:IUpdateTransactionService
    {
        private readonly IUpdateTransactionRepository _repository;

        public UpdateTransactionService(IUpdateTransactionRepository repository)
        {
            _repository = repository;
        }

        public void UpdateTransaction(Guid transactionId, int succeeded, string message)
        {
              _repository.UpdateTransaction(transactionId, succeeded, message);
        }
    }
}
