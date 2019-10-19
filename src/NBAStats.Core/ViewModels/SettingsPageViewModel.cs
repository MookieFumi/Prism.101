using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using NBAStats.Core.Events;
using NBAStats.Core.Services;
using NBAStats.Core.ViewModels.Base;
using NBAStats.Core.Views;
using Prism.Commands;
using Prism.Navigation;

namespace NBAStats.Core.ViewModels
{
    public class SettingsPageViewModel : BaseViewModel
    {
        private ObservableCollection<string> _teams;
        private readonly IPlayersService _playersService;
        private string _selectedTeam;
        private bool _sortByPointPerGame;

        public DelegateCommand LogoutCommand => new DelegateCommand(Logout);

        public SettingsPageViewModel(ICoreServices coreService, IPlayersService playersService) : base(coreService)
        {
            _playersService = playersService;
        }

        public override async Task InitializeAsync(INavigationParameters parameters)
        {
            var players = await _playersService.GetPlayers();
            var teams = new List<string> { "Show all teams" };
            teams.AddRange(players.OrderBy(p => p.TeamName).Select(p => p.TeamName).Distinct());
            Teams = new ObservableCollection<string>(teams);

            var context = ContextService.GetContext();
            SelectedTeam = context.PreferredTeam;
            SortByPointPerGame = context.SortByPointsPerGame;
        }

        #region Properties

        public bool SortByPointPerGame
        {
            get
            {
                return _sortByPointPerGame;
            }
            set
            {
                SetProperty(ref _sortByPointPerGame, value);
                EventAggregator.GetEvent<SortedByPPGEvent>().Publish(SortByPointPerGame);
                ContextService.AddSortByPointPerGame(SortByPointPerGame);
            }
        }

        public string SelectedTeam
        {
            get
            {
                return _selectedTeam;
            }
            set
            {
                SetProperty(ref _selectedTeam, value);
                if (_selectedTeam != null)
                {
                    EventAggregator.GetEvent<SelectedTeamEvent>().Publish(SelectedTeam);
                    ContextService.AddSelectedTeam(SelectedTeam);
                }
            }
        }

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
        #endregion

        private void Logout()
        {
            NavigationService.NavigateAsync($"/{nameof(LoginPage)}");
        }
    }
}
