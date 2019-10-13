using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Acr.UserDialogs;
using MonkeyCache.LiteDB;
using NBAStats.Core.Model;
using NBAStats.Core.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NBAStats.Core
{
    public partial class PlayersPage : ContentPage
    {
        public PlayersPage()
        {
            InitializeComponent();

            PlayersCollectionView.SelectionChanged += PlayersCollectionView_SelectionChanged;

            MainThread.BeginInvokeOnMainThread(async () =>
            {
                using (UserDialogs.Instance.Loading("Loading players..."))
                {
                    var service = new Services.Cache.PlayersService(Barrel.Current, DependencyService.Resolve<IPlayersService>());
                    IEnumerable<PlayerDTO> players = await service.GetPlayers();
                    PlayersCollectionView.ItemsSource = new ObservableCollection<PlayerDTO>(players);
                }
            });
        }

        private void PlayersCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (UserDialogs.Instance.Loading("Loading player..."))
            {
                Application.Current.MainPage.Navigation.PushAsync(new PlayerPage((PlayerDTO)e.CurrentSelection.First()));
            }
        }
    }
}
