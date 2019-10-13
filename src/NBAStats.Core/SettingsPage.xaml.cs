using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using MonkeyCache.LiteDB;
using NBAStats.Core.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NBAStats.Core
{
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();

            Task.Run(async () =>
            {
                var service = new Services.Cache.PlayersService(Barrel.Current, DependencyService.Resolve<IPlayersService>());

                var players = await service.GetPlayers();
                var teams = new List<string> { "Show all teams" };
                teams.AddRange(players.OrderBy(p => p.TeamName).Select(p => p.TeamName).Distinct());

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    TeamsPicker.ItemsSource = new ObservableCollection<string>(teams);
                    TeamsPicker.SelectedIndex = 0;
                });

            });
        }

        private void LogoutButton_Clicked(object sender, System.EventArgs e)
        {
            var page = Application.Current.MainPage.Navigation.NavigationStack.First();
            Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
            Application.Current.MainPage.Navigation.RemovePage(page);
        }
    }
}
