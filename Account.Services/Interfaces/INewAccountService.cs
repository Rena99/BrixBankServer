using Account.Services.Models;

namespace Account.Services.Interfaces
{
    public interface INewAccountService
    {
        bool AddCustomer(CustomerModel customerModel);
    }
}
