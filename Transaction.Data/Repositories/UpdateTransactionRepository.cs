using System;
using System.Linq;
using Transaction.Services.Interfaces;

namespace Transaction.Data.Repositories
{
    public class UpdateTransactionRepository:IUpdateTransactionRepository
    {
        private readonly TransactionContext _context;

        public UpdateTransactionRepository(TransactionContext context)
        {
            _context = context;
        }

        public void UpdateTransaction(Guid transactionId, int succeeded, string message)
        {
            Entities.Transaction transaction = _context.Transactions.FirstOrDefault(t => t.TransactionId == transactionId);
            transaction.Status = (Entities.Status)succeeded;
            transaction.FailureReason = message;
            _context.SaveChanges();
        }
    }
}
