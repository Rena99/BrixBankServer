using Account.Services.Interfaces;
using Account.Services.Models;
using System;
using System.Threading.Tasks;

namespace Account.Services.Services
{
    public class AccountInfoService : IAccountInfoService
    {
        private readonly IAccountInfoRepository _repository;

        public AccountInfoService(IAccountInfoRepository repository)
        {
            _repository = repository;
        }
     
        public async Task<AccountModel> GetAccount(Guid customerId)
        {
            return await _repository.GetAccount(customerId);
        }
    }
}
