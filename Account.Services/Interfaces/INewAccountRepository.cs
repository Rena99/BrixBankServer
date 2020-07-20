using Account.Services.Models;

namespace Account.Services.Interfaces
{
    public interface INewAccountRepository
    {
        bool AddCustomer(CustomerModel customerModel);
    }
}
