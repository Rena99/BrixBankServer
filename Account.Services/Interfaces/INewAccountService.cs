using Account.Services.Models;
using System.Threading.Tasks;

namespace Account.Services.Interfaces
{
    public interface INewAccountService
    {
        Task<bool> AddCustomer(VerificationHelperModel customerModel);
        void GetEmail(string email);
    }
}
