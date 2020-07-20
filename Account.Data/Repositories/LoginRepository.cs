using Account.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Account.Data.Repositories
{
   public class LoginRepository:ILoginRepository
    {
        private readonly AccountContext _context;

        public LoginRepository(AccountContext context)
        {
            _context = context;
        }

        public async Task<string> Login(string email, string password)
        {
            try
            {
                Guid customerId=new Guid();
                foreach (var item in _context.Customers)
                {
                    if (item.Email == email && Hashing.AreEqual(password, item.Password, item.Salt))
                    {
                        customerId = item.CustomerId;
                        break;
                    }
                }
                var currentAccount = await _context.Accounts.FirstOrDefaultAsync(c => c.CustomerId == customerId);
                return currentAccount.AccountId.ToString();
            }
            catch (Exception e)
            {
                return (e.Message);
            }
            throw new Exception("No such customer");
        }
   }
}
