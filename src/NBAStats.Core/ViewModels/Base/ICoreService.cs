using Acr.UserDialogs;
using NBAStats.Core.Services;
using Prism.Events;
using Prism.Navigation;

namespace NBAStats.Core.ViewModels.Base
{
    public interface ICoreServices
    {
        INavigationService NavigationService { get; }
        EventAggregator EventAggregator { get; }
        IUserDialogs UserDialogs { get; }
        IAppContextService ContextService { get; }
    }
}