using MonkeyCache.LiteDB;
using NBAStats.Core.Services;
using NBAStats.Core.ViewModels;
using Xamarin.Forms;

namespace NBAStats.Core.Views
{
    public partial class PlayersPage : ContentPage
    {
        public PlayersPage()
        {
            InitializeComponent();

            var playersService = new Services.Cache.PlayersService(Barrel.Current, DependencyService.Resolve<IPlayersService>());
            BindingContext = new PlayersPageViewModel(playersService);
        }
    }
}
