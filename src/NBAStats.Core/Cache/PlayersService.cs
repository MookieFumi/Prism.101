using System.Collections.Generic;
using System.Threading.Tasks;
using MonkeyCache;
using NBAStats.Core.Cache.Base;
using NBAStats.Core.Model;
using NBAStats.Core.Services;

namespace NBAStats.Core.Cache
{
    public class PlayersService : CacheBaseService, IPlayersService
    {
        private readonly IBarrel _barrel;
        private readonly IPlayersService _playersService;

        public PlayersService(IBarrel barrel, IPlayersService playersService)
        {
            _barrel = barrel;
            _playersService = playersService;
        }

        public async Task<IEnumerable<PlayerDTO>> GetPlayers()
        {
            var key = $"{this.ToString()}/{nameof(GetPlayers)}";

            if (!_barrel.IsExpired(key: key))
            {
                return _barrel.Get<IEnumerable<PlayerDTO>>(key: key);
            }

            var data = await _playersService.GetPlayers();

            AddToCache(_barrel, key, data);

            return data;
        }
    }
}
