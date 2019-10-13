using MonkeyCache.LiteDB;
using NBAStats.Core.Services;
using NBAStats.Core.ViewModels;
using NBAStats.Core.Views;
using Prism.Ioc;
using Xamarin.Forms;

namespace NBAStats.Core
{
    public static class ContainerRegistryExtensions
    {
        public static void RegisterPages(this IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<PlayersPage, PlayersPageViewModel>();
            containerRegistry.RegisterForNavigation<MyTabbedPage>();
            containerRegistry.RegisterForNavigation<PlayerPage, PlayerPageViewModel>();
            containerRegistry.RegisterForNavigation<SettingsPage, SettingsPageViewModel>();
        }

        public static void RegisterServices(this IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<ILoginService, LoginService>();
            containerRegistry.RegisterInstance<IPlayersService>(new Services.Cache.PlayersService(Barrel.Current, new PlayersService()));
        }
    }
}
