using Prism.Mvvm;
using Prism.Navigation;

namespace NBAStats.Core.ViewModels.Base
{
    public class BaseViewModel : BindableBase
    {
        public BaseViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        protected INavigationService NavigationService { get; }
    }
}
