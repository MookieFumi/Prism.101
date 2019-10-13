using MonkeyCache.LiteDB;
using NBAStats.Core.Services;
using NBAStats.Core.ViewModels;
using Xamarin.Forms;

namespace NBAStats.Core.Views
{
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();

            var playersService = new Services.Cache.PlayersService(Barrel.Current, DependencyService.Resolve<IPlayersService>());
            BindingContext = new SettingsPageViewModel(playersService);
        }
    }
}
