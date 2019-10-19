using NBAStats.Core.Context;

namespace NBAStats.Core.Services
{
    public interface IAppContextService
    {
        AppContext GetContext();
        void SaveContext(string email, string apiUrl, bool useCacheServices, bool allowSendStats);
        void AddSortByPointPerGame(bool sortByPointPerGame);
        void AddSelectedTeam(string selectedTeam);
    }
}