using System.Threading.Tasks;
using Acr.UserDialogs;
using NBAStats.Core.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NBAStats.Core
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void PasswordEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoginButton.IsEnabled = !string.IsNullOrEmpty(UsernameEntry.Text) && !string.IsNullOrEmpty(PasswordEntry.Text);
        }

        private void UsernameEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoginButton.IsEnabled = !string.IsNullOrEmpty(UsernameEntry.Text) && !string.IsNullOrEmpty(PasswordEntry.Text);
        }

        private void LoginButton_Clicked(object sender, System.EventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                using (UserDialogs.Instance.Loading("Wait, login you..."))
                {
                    var request = new Model.LoginRequestDTO
                    {
                        Username = UsernameEntry.Text,
                        Password = PasswordEntry.Text,
                        AllowSendStats = AllowSendStats.IsChecked,
                        UseCacheServices = UseCacheServices.IsChecked
                    };

                    var service = DependencyService.Resolve<ILoginService>();
                    if (await service.Login(request))
                    {
                        await Application.Current.MainPage.Navigation.PushAsync(new MyTabbedPage());
                        Application.Current.MainPage.Navigation.RemovePage(this);
                    }
                }
            });
        }
    }
}
