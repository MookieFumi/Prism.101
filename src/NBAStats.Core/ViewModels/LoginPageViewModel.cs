using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using NBAStats.Core.Services;
using NBAStats.Core.ViewModels.Base;
using NBAStats.Core.Views;
using Xamarin.Forms;

namespace NBAStats.Core.ViewModels
{
    public class LoginPageViewModel : BaseViewModel
    {
        public ICommand LoginCommand { get; private set; }

        public LoginPageViewModel(ILoginService loginService)
        {
            LoginCommand = new Command(async () => await Login());
            this.loginService = loginService;
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

                if (await loginService.Login(request))
                {
                    var page = Application.Current.MainPage.Navigation.NavigationStack.First();
                    await Application.Current.MainPage.Navigation.PushAsync(new MyTabbedPage());
                    Application.Current.MainPage.Navigation.RemovePage(page);
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
                _username = value;
                OnPropertyChanged();
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
                _password = value;
                OnPropertyChanged();
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
                _allowSendStats = value;
                OnPropertyChanged();
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
                _useCacheServices = value;
                OnPropertyChanged();
            }
        }

        private bool _canLogin;
        private readonly ILoginService loginService;

        public bool CanLogin
        {
            get
            {
                return _canLogin;
            }
            set
            {
                _canLogin = value;
                OnPropertyChanged();
            }
        }
    }
}
