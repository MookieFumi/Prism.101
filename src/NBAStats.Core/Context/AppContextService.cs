using NBAStats.Core.Context;

namespace NBAStats.Core.Services
{
    public class AppContextService : IAppContextService
    {
        public void SaveContext(string email, string apiUrl, bool useCacheServices, bool allowSendStats)
        {
            var context = GetContext();
            context.Email = email;
            context.ApiUrl = apiUrl;
            context.UseCacheServices = useCacheServices;
            context.AllowSendStats = allowSendStats;

            Persistence.Context = context;
        }

        public AppContext GetContext()
        {
            return Persistence.Context;
        }

        public void AddSortByPointPerGame(bool sortByPointPerGame)
        {
            var context = GetContext();
            context.SortByPointsPerGame = sortByPointPerGame;

            Persistence.Context = context;
        }

        public void AddSelectedTeam(string selectedTeam)
        {
            var context = GetContext();
            context.PreferredTeam = selectedTeam;

            Persistence.Context = context;
        }
    }
}
