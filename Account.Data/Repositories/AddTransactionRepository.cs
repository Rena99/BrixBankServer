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

        public async Task<string> AddTransaction(Guid fromAccount, Guid toAccount, int amount)
        {
            try
            {
                Entities.Account FromAccount = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountId == fromAccount);
                Entities.Account ToAccount = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountId == toAccount);
                if (FromAccount != null)
                {
                    if (ToAccount != null)
                    {
                        if (FromAccount.Balance >= amount)
                        {
                            FromAccount.Balance -= amount;
                            ToAccount.Balance -= amount;
                            _context.SaveChanges();
                            return "";
                        }
                        else
                        {
                            return "Not enough Money in the account";
                        }
                    }
                    else
                    {
                        return "To Account Does Not Exist";
                    }
                }
                else
                {
                    return "From Account Does Not Exist";
                }
            }
            catch(Exception e)
            {
                return e.Message;
            }
        }
    }
}
