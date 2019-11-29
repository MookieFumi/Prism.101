using System;
using System.Threading.Tasks;
using NBAStats.Core.Model;
using NBAStats.Core.Services;
using NBAStats.Core.ViewModels.Base;
using NBAStats.Core.Views;
using Prism.Navigation;

namespace NBAStats.Core.ViewModels
{
    public class LoginPageViewModel : BaseViewModel
    {
        private string _username;
        private string _password;
        private bool _canLogin;
        private bool _allowSendStats;
        private bool _useCacheServices;

        private readonly ILoginService _loginService;

        public DelegateCommandAsync LoginCommand { get; private set; }

        public LoginPageViewModel(ICoreServices coreService, ILoginService loginService) : base(coreService)
        {
            _loginService = loginService;

#if DEBUG
            Username = "mookiefumi@nba.com";
            Password = "nba@2019";
#endif

            LoginCommand = new DelegateCommandAsync(async () => await Login(), () => Task.FromResult(CanLogin));
        }

        private async Task Login()
        {
            var request = new Model.LoginRequestDTO
            {
                Username = Username,
                Password = Password,
                AllowSendStats = AllowSendStats,
                UseCacheServices = UseCacheServices
            };

            try
            {
                LoginResponseDTO response;
                using (UserDialogs.Loading("Please wait, be patient ..."))
                {
                    response = await _loginService.Login(request);
                }
                if (response.IsValid)
                {
                    ContextService.SaveContext(Username, response.ApiUrl, UseCacheServices, AllowSendStats);
                    await NavigationService.NavigateAsync($"{nameof(MyTabbedPage)}");
                    //await NavigationService.NavigateAsync($"{nameof(MyTabbedPage)}?{KnownNavigationParameters.SelectedTab}={nameof(SettingsPage)}");
                }
                else
                {
                    UserDialogs.Toast("Invalid login, please try it again");
                }
            }
            catch (Exception exception)
            {
                await HandleException(exception);
            }
        }

        #region Properties
        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                SetProperty(ref _username, value);
                CanLogin = !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                SetProperty(ref _password, value);
                CanLogin = !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);
            }
        }

        public bool AllowSendStats
        {
            get
            {
                return _allowSendStats;
            }
            set
            {
                SetProperty(ref _allowSendStats, value);
            }
        }

        public bool UseCacheServices
        {
            get
            {
                return _useCacheServices;
            }
            set
            {
                SetProperty(ref _useCacheServices, value);
            }
        }

        public bool CanLogin
        {
            get
            {
                return _canLogin;
            }
            set
            {
                SetProperty(ref _canLogin, value);
            }
        }
        #endregion
    }
}
