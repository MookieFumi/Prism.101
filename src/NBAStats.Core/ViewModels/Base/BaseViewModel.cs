using System;
using System.Threading.Tasks;
using Acr.UserDialogs;
using NBAStats.Core.Services;
using NBAStats.Core.Services.Exceptions;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;

namespace NBAStats.Core.ViewModels.Base
{
    public class BaseViewModel : BindableBase, IInitializeAsync, INavigationAware
    {
        public BaseViewModel(ICoreServices coreService)
        {
            NavigationService = coreService.NavigationService;
            EventAggregator = coreService.EventAggregator;
            UserDialogs = coreService.UserDialogs;
            ContextService = coreService.ContextService;
        }

        protected INavigationService NavigationService { get; }
        protected EventAggregator EventAggregator { get; }
        protected IUserDialogs UserDialogs { get; }
        protected IAppContextService ContextService { get; }

        public virtual Task InitializeAsync(INavigationParameters parameters)
        {
            return Task.CompletedTask;
        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {
        }

        protected async Task HandleException(Exception exception)
        {
            await Task.Delay(0);

            if (exception is NotConnectivityException)
            {
                UserDialogs.Toast("Check your internet connection");
                return;
            }

            UserDialogs.Toast("There was an error calling the API");
        }
    }
}
