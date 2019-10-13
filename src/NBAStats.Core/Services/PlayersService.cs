using System.Collections.Generic;
using System.Threading.Tasks;
using NBAStats.Core.Model;
using Refit;

namespace NBAStats.Core.Services
{
    public class PlayersService : IPlayersService
    {
        private const string baseUrl = "https://nba-players.herokuapp.com";

        public async Task<IEnumerable<PlayerDTO>> GetPlayers()
        {
            var service = RestService.For<IPlayersService>(baseUrl);

            var result = await service.GetPlayers();

            return result;
        }
    }
}
