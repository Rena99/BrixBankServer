using System;
using System.Linq;
using Account.Data.Entities;
using Account.Services.Interfaces;

namespace Account.Data.Repositories
{
    public class AddHistoryRepository : IAddHistoryRepository
    {
        private readonly AccountContext _context;

        public AddHistoryRepository(AccountContext context)
        {
            _context = context;
        }
        public void AddHistory(Guid fromAccount, Guid toAccount, int amount, Guid transactionId)
        {
            _context.OperationsHistory.Add(new OperationHistory()
            {
                OperationHistoryId = Guid.NewGuid(),
                AccountId = fromAccount,
                OperationTime = DateTime.Now,
                Debit = true,
                Balance = _context.Accounts.FirstOrDefault(a => a.AccountId == fromAccount).Balance,
                TransactionAmount = amount,
                TransactionId = transactionId,
            });
            _context.OperationsHistory.Add(new OperationHistory()
            {
                OperationHistoryId = Guid.NewGuid(),
                AccountId = toAccount,
                OperationTime = DateTime.Now,
                Debit = false,
                Balance = _context.Accounts.FirstOrDefault(a => a.AccountId == toAccount).Balance,
                TransactionAmount = amount,
                TransactionId = transactionId,
            });
            _context.SaveChanges();
        }
    }
}
