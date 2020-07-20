using System.Threading.Tasks;

namespace Account.Services.Interfaces
{
    public interface ILoginRepository
    {
        Task<string> Login(string email, string password);
    }
}
