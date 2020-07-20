using Account.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
                foreach (var item in _context.Customers)
                {
                    if (item.Email == email && Hashing.AreEqual(password, item.Password, item.Salt))
                    {
                        var currentAccount = await _context.Accounts.FirstOrDefaultAsync(c => c.CustomerId == item.CustomerId);
                        return currentAccount.AccountId.ToString();
                    }
                }
            }
            catch (Exception e)
            {
                return (e.Message);
            }
            throw new Exception("No such customer");
        }
   }
}
