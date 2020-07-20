using Account.Services.Interfaces;
using Account.Services.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Account.Data.Repositories
{
    public class AccountInfoRepository : IAccountInfoRepository
    {
        private readonly AccountContext _context;
        private readonly IMapper _mapper;

        public AccountInfoRepository(AccountContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<AccountModel> GetAccount(Guid CustomerId)
        {
            try
            {
                var account = await _context.Accounts.Include(c => c.Customer).FirstOrDefaultAsync(c => c.Customer.CustomerId == CustomerId);
                AccountModel accountModel = _mapper.Map<AccountModel>(account);
                if (accountModel != null)
                {
                    return accountModel;
                }
                else
                {
                    throw new Exception("Account not Found");
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }

   
    }
}
