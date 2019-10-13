using MonkeyCache.LiteDB;
using NBAStats.Core.Services;
using NBAStats.Core.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NBAStats.Core
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            InitializeCache();

            DependencyService.Register<ILoginService, LoginService>();
            DependencyService.Register<IPlayersService, PlayersService>();

            MainPage = new NavigationPage(new LoginPage());
        }

        private static void InitializeCache()
        {
            Barrel.ApplicationId = nameof(NBAStats);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
