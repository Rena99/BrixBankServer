using System;
using System.Threading.Tasks;
using Account.Data.Entities;
using Account.Services.Interfaces;
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

        public bool CheckAccountExistance(Entities.Account account)
        {
            if (account == null)
            {
                throw new Exception("No such account found");
            }
            return true;
        }

        public bool CheckBalance(int amount, int balance)
        {
            if (amount>=balance)
            {
                throw new Exception("Not enough money");
            }
            return true;
        }

        public async Task<int> AddTransaction(Guid fromAccount, Guid toAccount, int amount)
        {
            try
            {
                Entities.Account fAccount = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountId == fromAccount);
                Entities.Account tAccount = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountId == toAccount);
                if (!CheckAccountExistance(fAccount) || !CheckAccountExistance(tAccount))
                {
                    return 2;
                }
                if (!CheckBalance(amount, fAccount.Balance))
                {
                    return 2;
                }
                fAccount.Balance -= amount;
                tAccount.Balance -= amount;
                _context.SaveChangesAsync();
                return 1;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
