using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Acr.UserDialogs;
using NBAStats.Core.Events;
using NBAStats.Core.Model;
using NBAStats.Core.Services;
using NBAStats.Core.ViewModels.Base;
using NBAStats.Core.Views;
using Newtonsoft.Json.Linq;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;

namespace NBAStats.Core.ViewModels
{
    public class PlayersPageViewModel : BaseViewModel
    {
        private PlayerDTO _selectedPlayer;
        private ObservableCollection<PlayerDTO> _players;
        private readonly IPlayersService _playersService;

        private string _preferredTeam = "Show all teams";
        private bool _sortByPPG;

        public DelegateCommand NavigateToPlayerCommand { get; set; }

        public PlayersPageViewModel(ICoreServices coreService, IPlayersService playersService) : base(coreService)
        {
            _playersService = playersService;

            NavigateToPlayerCommand = new DelegateCommand(async () => await NavigateToPlayer());

            EventAggregator.GetEvent<SelectedTeamEvent>().Subscribe(async (teamName) =>
            {
                _preferredTeam = teamName;
                await LoadTeams();
            });

            EventAggregator.GetEvent<SortedByPPGEvent>().Subscribe(async (sortByPPG) =>
            {
                _sortByPPG = sortByPPG;
                await LoadTeams();
            });
        }

        public override async Task InitializeAsync(INavigationParameters parameters)
        {
            LoadSettings();
            await LoadTeams();
        }

        ~PlayersPageViewModel()
        {
            EventAggregator.GetEvent<SelectedTeamEvent>().Unsubscribe((teamName) => { });
            EventAggregator.GetEvent<SortedByPPGEvent>().Unsubscribe((sortByPPG) => { });
        }

        private void LoadSettings()
        {
            var context = ContextService.GetContext();

            _sortByPPG = context.SortByPointsPerGame;
            _preferredTeam = context.PreferredTeam;
        }

        private async Task LoadTeams()
        {
            var players = await _playersService.GetPlayers();

            if (_preferredTeam != "Show all teams")
            {
                players = players.Where(p => p.TeamName == _preferredTeam);
            }

            if (_sortByPPG)
            {
                players = players.OrderByDescending(p => p.PointsPerGame);
            }

            Players = new ObservableCollection<PlayerDTO>(players);
        }

        private async Task NavigateToPlayer()
        {
            var @params = new NavigationParameters
                {
                    { "player", SelectedPlayer }
                };

            await NavigationService.NavigateAsync($"NavigationPage/PlayersPage/{nameof(PlayerPage)}", @params);
        }

        #region Properties
        public PlayerDTO SelectedPlayer
        {
            get
            {
                return _selectedPlayer;
            }
            set
            {
                SetProperty(ref _selectedPlayer, value);
            }
        }

        public ObservableCollection<PlayerDTO> Players
        {
            get
            {
                return _players;
            }
            set
            {
                SetProperty(ref _players, value);
            }
        }
        #endregion
    }
}
