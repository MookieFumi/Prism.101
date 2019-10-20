using Acr.UserDialogs;
using Moq;
using NBAStats.Core.Services;
using NBAStats.Core.ViewModels.Base;
using Prism.Events;
using Prism.Navigation;

namespace NBAStats.Core.Test.ViewModels.Base
{
    public abstract class BaseViewModelTest
    {
        protected ICoreServices CoreServices;

        protected Mock<INavigationService> NavigationService;
        protected Mock<IUserDialogs> UserDialogs;
        protected Mock<IAppContextService> ContextService;

        protected BaseViewModelTest()
        {
            ContextService = new Mock<IAppContextService>();
            UserDialogs = new Mock<IUserDialogs>();
            NavigationService = new Mock<INavigationService>();

            CoreServices = new CoreServices(
                NavigationService.Object,
                new Mock<EventAggregator>().Object,
                UserDialogs.Object,
                ContextService.Object);
        }
    }
}
