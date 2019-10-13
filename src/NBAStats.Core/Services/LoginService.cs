using System.Threading.Tasks;
using NBAStats.Core.Model;

namespace NBAStats.Core.Services
{
    public class LoginService : ILoginService
    {
        public async Task<bool> Login(LoginRequestDTO request)
        {
            await Task.Delay(999);
            return !string.IsNullOrEmpty(request.Username) && !string.IsNullOrEmpty(request.Password);
        }
    }


}
