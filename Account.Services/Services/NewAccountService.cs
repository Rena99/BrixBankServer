using Account.Services.Interfaces;
using Account.Services.Models;

namespace Account.Services.Services
{
    public class NewAccountService:INewAccountService
    {
        private readonly INewAccountRepository _repository;

        public NewAccountService(INewAccountRepository repository)
        {
            _repository = repository;
        }

        public bool AddCustomer(CustomerModel customerModel)
        {
            return _repository.AddCustomer(customerModel);
        }
    }
}
