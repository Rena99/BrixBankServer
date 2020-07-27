using System;
using System.Threading.Tasks;
using Account.Services.Interfaces;
using Messages;
using Microsoft.EntityFrameworkCore;

namespace Account.Data.Repositories
{
    public class AddTransactionRepository : IAddTransactionRepository
    {
        private readonly AccountContext _context;

        public AddTransactionRepository(AccountContext context)
        {
            _context = context;
        }

        public async Task<UpdateTransaction> AddTransaction(AddTransaction message)
        {
            UpdateTransaction transaction = new UpdateTransaction
            {
                Succeeded = 2
            };
            try
            {
                Entities.Account FromAccount = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountId == message.FromAccount);
                Entities.Account ToAccount = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountId == message.ToAccount);
                if (FromAccount != null)
                {
                    if (ToAccount != null)
                    {
                        if (FromAccount.Balance >= message.Amount)
                        {
                            FromAccount.Balance -= message.Amount;
                            ToAccount.Balance -= message.Amount;
                            _context.SaveChanges();
                            transaction.Succeeded = 1;
                        }
                        else
                        {
                            transaction.Message = "Not enough Money in the account";
                        }
                    }
                    else
                    {
                        transaction.Message = "To Account Does Not Exist";
                    }
                }
                else
                {
                    transaction.Message = "From Account Does Not Exist";
                }
            }
            catch(Exception e)
            {
                transaction.Message = e.Message;
            }
            return transaction;
        }
    }
}
