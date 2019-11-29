using System;
using System.Collections.Generic;
using System.Linq;
using Acr.UserDialogs;
using MonkeyCache;
using MonkeyCache.LiteDB;
using NBAStats.Core;
using NBAStats.Core.Services;
using NBAStats.Core.ViewModels;
using NBAStats.Core.ViewModels.Base;
using NBAStats.Core.Views;
using Prism.Events;
using Unity;
using Xamarin.Forms;

namespace Prism.Ioc
{
    public static class ContainerRegistryExtensions
    {
        public static void RegisterPages(this IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MyTabbedPage>();

            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<PlayersPage, PlayersPageViewModel>();
            containerRegistry.RegisterForNavigation<PlayerPage, PlayerPageViewModel>();
            containerRegistry.RegisterForNavigation<SettingsPage, SettingsPageViewModel>();

            //containerRegistry.RegisterAllRegisterablePages();
        }

        public static void RegisterServices(this IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance<IBarrel>(Barrel.Current);
            containerRegistry.RegisterInstance<IUserDialogs>(UserDialogs.Instance);
            containerRegistry.RegisterSingleton<EventAggregator>();

            containerRegistry.RegisterSingleton<ICoreServices, CoreServices>();
            containerRegistry.Register<IConnectivityService, ConnectivityService>();
            containerRegistry.RegisterSingleton<IAppContextService, AppContextService>();
            containerRegistry.Register<ILoginService, LoginService>();

            var container = ((IContainerExtension<IUnityContainer>)containerRegistry).Instance;
            containerRegistry.RegisterInstance<IPlayersService>(new NBAStats.Core.Cache.PlayersService(container.Resolve<IBarrel>(), new PlayersService(container.Resolve<IAppContextService>(), container.Resolve<IConnectivityService>())));
        }

        private static void RegisterAllRegisterablePages(this IContainerRegistry containerRegistry)
        {
            var pages = GetClasses<IRegisterablePage>();
            foreach (var page in pages)
            {
                containerRegistry.RegisterForNavigation(page, page.Name);
            }
        }

        private static IEnumerable<Type> GetClasses<T>()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => p.IsClass && typeof(T).IsAssignableFrom(p));
        }
    }
}
