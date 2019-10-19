namespace NBAStats.Core.Services.Base
{
    public abstract class BaseService
    {
        protected BaseService(IAppContextService contextService, IConnectivityService connectivityService)
        {
            ContextService = contextService;
            ConnectivityService = connectivityService;
        }

        protected IAppContextService ContextService { get; }
        protected IConnectivityService ConnectivityService { get; }
    }
}
