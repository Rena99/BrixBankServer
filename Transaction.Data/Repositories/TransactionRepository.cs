using System.Threading.Tasks;
using System;
using Transaction.Services.Interfaces;
using Transaction.Services.Models;
using AutoMapper;

namespace Transaction.Data.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly TransactionContext _context;
        private readonly IMapper _mapper;

        public TransactionRepository(TransactionContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Guid> AddTransaction(TransactionModel transactionModel)
        {
            Entities.Transaction transaction = _mapper.Map<Entities.Transaction>(transactionModel);
            transaction.Date = DateTime.Now;
            transaction.Status = 0;
            transaction.TransactionId = Guid.NewGuid();
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            return transaction.TransactionId;
        }
    }
}
