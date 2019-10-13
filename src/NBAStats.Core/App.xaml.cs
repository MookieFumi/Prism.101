using MonkeyCache.LiteDB;
using NBAStats.Core.Views;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NBAStats.Core
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class App : PrismApplication
    {
        public App() : base(null)
        {
        }

        public App(IPlatformInitializer initializer) : base(initializer)
        {
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

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            InitializeCache();

            containerRegistry.RegisterPages();
            containerRegistry.RegisterServices();
        }

        protected override void OnInitialized()
        {
            InitializeComponent();

            NavigationService.NavigateAsync($"{nameof(LoginPage)}");
        }
    }
}
