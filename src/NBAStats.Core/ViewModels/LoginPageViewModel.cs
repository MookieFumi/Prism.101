using System.Threading.Tasks;
using Acr.UserDialogs;
using NBAStats.Core.Services;
using NBAStats.Core.ViewModels.Base;
using NBAStats.Core.Views;
using Prism.Commands;
using Prism.Navigation;

namespace NBAStats.Core.ViewModels
{
    public class LoginPageViewModel : BaseViewModel
    {
        public DelegateCommand LoginCommand { get; private set; }

        public LoginPageViewModel(INavigationService navigationService, ILoginService loginService) : base(navigationService)
        {
            LoginCommand = new DelegateCommand(async () => await Login());
            _loginService = loginService;
        }

        private async Task Login()
        {
            using (UserDialogs.Instance.Loading("Wait, login you..."))
            {
                var request = new Model.LoginRequestDTO
                {
                    Username = Username,
                    Password = Password,
                    AllowSendStats = AllowSendStats,
                    UseCacheServices = UseCacheServices
                };

                if (await _loginService.Login(request))
                {
                    await NavigationService.NavigateAsync($"/NavigationPage/{nameof(MyTabbedPage)}?{KnownNavigationParameters.SelectedTab}={nameof(SettingsPage)}");
                }
            }
        }

        private string _username;
        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                SetProperty(ref _username, value);
                if (!string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password))
                {
                    CanLogin = true;
                }
            }
        }

        private string _password;

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                SetProperty(ref _password, value);
                if (!string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password))
                {
                    CanLogin = true;
                }
            }
        }

        private bool _allowSendStats;

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

        private bool _useCacheServices;

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

        private bool _canLogin;
        private readonly ILoginService _loginService;

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
    }
}
