using System.Threading.Tasks;
using NBAStats.Core.Model;
using NBAStats.Core.Services.Base;
using NBAStats.Core.Services.Exceptions;

namespace NBAStats.Core.Services
{
    public class LoginService : BaseService, ILoginService
    {
        public LoginService(IAppContextService contextService, IConnectivityService connectivityService) : base(contextService, connectivityService)
        {
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO request)
        {
            if (!ConnectivityService.HasConnection())
            {
                throw new NotConnectivityException();
            }

            await Task.Delay(999);

            return new LoginResponseDTO
            {
                IsValid = !string.IsNullOrEmpty(request.Username) && !string.IsNullOrEmpty(request.Password),
                ApiUrl = "https://nba-players.herokuapp.com"
            };
        }
    }
}
