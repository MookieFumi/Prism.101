using System.Threading.Tasks;
using NBAStats.Core.Model;

namespace NBAStats.Core.Services
{
    public interface ILoginService
    {
        Task<LoginResponseDTO> Login(LoginRequestDTO request);
    }
}