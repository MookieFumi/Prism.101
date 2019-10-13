using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using NBAStats.Core.Services;
using NBAStats.Core.ViewModels.Base;
using NBAStats.Core.Views;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Essentials;

namespace NBAStats.Core.ViewModels
{
    public class SettingsPageViewModel : BaseViewModel
    {
        public DelegateCommand LogoutCommand => new DelegateCommand(Logout);

        public SettingsPageViewModel(INavigationService navigationService, IPlayersService playersService) : base(navigationService)
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
            NavigationService.NavigateAsync($"/{nameof(LoginPage)}");
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
                SetProperty(ref _sortByPointPerGame, value);
            }
        }

        private string _selectedTeam;

        public string SelectedTeam
        {
            get
            {
                return _selectedTeam;
            }
            set
            {
                SetProperty(ref _selectedTeam, value);
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
                SetProperty(ref _teams, value);
            }
        }
    }
}
