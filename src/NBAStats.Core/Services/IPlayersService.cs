using System.Collections.Generic;
using System.Threading.Tasks;
using NBAStats.Core.Model;
using Refit;

namespace NBAStats.Core.Services
{
    public interface IPlayersService
    {
        [Get("/players-stats")]
        Task<IEnumerable<PlayerDTO>> GetPlayers();
    }
}
