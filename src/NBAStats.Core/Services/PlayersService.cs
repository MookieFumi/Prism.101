using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NBAStats.Core.Model;
using NBAStats.Core.Services.Base;
using NBAStats.Core.Services.Exceptions;
using Refit;

namespace NBAStats.Core.Services
{
    public class PlayersService : BaseService, IPlayersService
    {
        public PlayersService(IAppContextService contextService, IConnectivityService connectivityService) : base(contextService, connectivityService)
        {
        }

        public async Task<IEnumerable<PlayerDTO>> GetPlayers()
        {
            if (!ConnectivityService.HasConnection())
            {
                throw new NotConnectivityException();
            }
            try
            {
                var context = ContextService.GetContext();

                var service = RestService.For<IPlayersService>(context.ApiUrl);

                var result = await service.GetPlayers();

                return result;
            }
            catch (Exception exception)
            {
                throw new ServiceException(exception.Message);
            }
        }
    }
}
