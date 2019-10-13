using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MonkeyCache.LiteDB;
using NBAStats.Core.Services;
using NBAStats.Core.ViewModels.Base;
using NBAStats.Core.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NBAStats.Core.ViewModels
{
    public class SettingsPageViewModel : BaseViewModel
    {
        public ICommand LogoutCommand => new Command(Logout);

        public SettingsPageViewModel(IPlayersService playersService)
        {
            Task.Run(async () =>
            {
                var players = await _playersService.GetPlayers();
                var teams = new List<string> { "Show all teams" };
                teams.AddRange(players.OrderBy(p => p.TeamName).Select(p => p.TeamName).Distinct());

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    Teams = new ObservableCollection<string>(teams);
                    SelectedTeam = Teams.First();
                });

            });
            _playersService = playersService;
        }

        private void Logout()
        {
            var page = Application.Current.MainPage.Navigation.NavigationStack.First();
            Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
            Application.Current.MainPage.Navigation.RemovePage(page);
        }

        private bool _sortByPointPerGame;

        public bool SortByPointPerGame
        {
            get
            {
                return _sortByPointPerGame;
            }
            set
            {
                _sortByPointPerGame = value;
                OnPropertyChanged();
            }
        }

        private string _seletecTeam;

        public string SelectedTeam
        {
            get
            {
                return _seletecTeam;
            }
            set
            {
                _seletecTeam = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _teams;
        private readonly IPlayersService _playersService;

        public ObservableCollection<string> Teams
        {
            get
            {
                return _teams;
            }
            set
            {
                _teams = value;
                OnPropertyChanged();
            }
        }
    }
}
