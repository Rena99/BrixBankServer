using Account.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace Account.Services.Services
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _repository;

        public LoginService(ILoginRepository repository)
        {
            _repository = repository;
        }

        public async Task<string> Login(string email, string password)
        {
            if (await _repository.Login(email, password) != null)
            {
                return await _repository.Login(email, password);
            }
            else
            {
                throw new Exception("Customer not found");
            }
        }
    }
}
