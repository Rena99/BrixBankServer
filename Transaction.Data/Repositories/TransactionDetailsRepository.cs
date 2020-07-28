using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Transaction.Services.Interfaces;
using Transaction.Services.Models;

namespace Transaction.Data.Repositories
{
    public class TransactionDetailsRepository: ITransactionDetailsRepository
    {
        private readonly TransactionContext _context;
        private readonly IMapper _mapper;

        public TransactionDetailsRepository(TransactionContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TransactionModel> GetTransaction(Guid id)
        {
            return _mapper.Map<TransactionModel>(await _context.Transactions.FirstOrDefaultAsync(t => t.TransactionId == id));
        }
    }
}
