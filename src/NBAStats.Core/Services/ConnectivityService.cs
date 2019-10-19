using Xamarin.Essentials;

namespace NBAStats.Core.Services
{
    public class ConnectivityService : IConnectivityService
    {
        public bool HasConnection()
        {
            var current = Connectivity.NetworkAccess;
            return current == NetworkAccess.Internet;
        }
    }
}
