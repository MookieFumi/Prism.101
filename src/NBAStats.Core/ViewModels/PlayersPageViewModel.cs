using System.Collections.ObjectModel;
using System.Windows.Input;
using Acr.UserDialogs;
using MonkeyCache.LiteDB;
using NBAStats.Core.Model;
using NBAStats.Core.Services;
using NBAStats.Core.ViewModels.Base;
using NBAStats.Core.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NBAStats.Core.ViewModels
{
    public class PlayersPageViewModel : BaseViewModel
    {
        public ICommand NavigateToPlayerCommand { get; set; }

        public PlayersPageViewModel(IPlayersService playersService)
        {
            _playersService = playersService;
            NavigateToPlayerCommand = new Command(NavigateToPlayer);

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
                Application.Current.MainPage.Navigation.PushAsync(new PlayerPage(SelectedPlayer));
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
                _selectedPlayer = value;
                OnPropertyChanged();
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
                _players = value;
                OnPropertyChanged();
            }
        }
    }
}
