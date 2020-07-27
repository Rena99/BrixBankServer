using System.Threading.Tasks;

namespace Account.Services.Interfaces
{
    public interface ILoginService
    {
        Task<string> Login(string email, string password);
    }
}
