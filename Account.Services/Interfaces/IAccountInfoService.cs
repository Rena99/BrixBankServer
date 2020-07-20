using Account.Services.Models;
using System;
using System.Threading.Tasks;

namespace Account.Services.Interfaces
{
    public interface IAccountInfoService
    {
        Task<AccountModel> GetAccount(Guid accountId);
    }
}
