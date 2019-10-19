using Acr.UserDialogs;
using NBAStats.Core.Services;
using Prism.Events;
using Prism.Navigation;

namespace NBAStats.Core.ViewModels.Base
{
    public class CoreServices : ICoreServices
    {
        public CoreServices(INavigationService navigationService, EventAggregator eventAggregator, IUserDialogs userDialogs, IAppContextService contextService)
        {
            NavigationService = navigationService;
            EventAggregator = eventAggregator;
            UserDialogs = userDialogs;
            ContextService = contextService;
        }

        public INavigationService NavigationService { get; }
        public EventAggregator EventAggregator { get; }
        public IUserDialogs UserDialogs { get; }
        public IAppContextService ContextService { get; }
    }
}
