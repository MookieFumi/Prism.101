using System.Collections.ObjectModel;
using Acr.UserDialogs;
using NBAStats.Core.Model;
using NBAStats.Core.Services;
using NBAStats.Core.ViewModels.Base;
using NBAStats.Core.Views;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Essentials;

namespace NBAStats.Core.ViewModels
{

    public class PlayersPageViewModel : BaseViewModel
    {
        public DelegateCommand NavigateToPlayerCommand { get; set; }

        public PlayersPageViewModel(INavigationService navigationService, IPlayersService playersService) : base(navigationService)
        {
            _playersService = playersService;
            NavigateToPlayerCommand = new DelegateCommand(NavigateToPlayer);

            MainThread.BeginInvokeOnMainThread(async () =>
            {
                using (UserDialogs.Instance.Loading("Loading players..."))
                {
                    var players = await _playersService.GetPlayers();
                    Players = new ObservableCollection<PlayerDTO>(players);
                }
            });
        }

        private void NavigateToPlayer()
        {
            using (UserDialogs.Instance.Loading("Loading player..."))
            {
                var @params = new NavigationParameters
                {
                    { "player", SelectedPlayer }
                };
                NavigationService.NavigateAsync(nameof(PlayerPage), @params);
            }
        }

        private PlayerDTO _selectedPlayer;

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

        private ObservableCollection<PlayerDTO> _players;
        private readonly IPlayersService _playersService;

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
    }
}
