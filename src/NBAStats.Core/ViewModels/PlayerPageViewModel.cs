using System.Threading.Tasks;
using NBAStats.Core.Model;
using NBAStats.Core.ViewModels.Base;
using Prism.Navigation;

namespace NBAStats.Core.ViewModels
{
    public class PlayerPageViewModel : BaseViewModel
    {
        private string _name;
        private string _minutesPerGame;
        private string _image;
        private decimal _pointsPerGame;
        private string _reboundsPerGame;
        private string _assistsPerGame;
        private string _stealsPerGame;
        private string _blocksPerGame;

        public PlayerPageViewModel(ICoreServices coreService) : base(coreService)
        {
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
        }

        public override async Task InitializeAsync(INavigationParameters parameters)
        {
            //    var player = parameters.GetValue<PlayerDTO>("player");

            //    //Name = player.Name;
            //    //MinutesPerGame = player.MinutesPerGame;
            //    //Image = player.Image;
            //    //PointsPerGame = player.PointsPerGame;
            //    //ReboundsPerGame = player.ReboundsPerGame;
            //    //AssistsPerGame = player.AssistsPerGame;
            //    //StealsPerGame = player.StealsPerGame;
            //    //BlocksPerGame = player.BlocksPerGame;

            await base.InitializeAsync(parameters);
        }

        #region Properties
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                SetProperty(ref _name, value);
            }
        }

        public string MinutesPerGame
        {
            get
            {
                return _minutesPerGame;
            }
            set
            {
                SetProperty(ref _minutesPerGame, value);
            }
        }

        public string Image
        {
            get
            {
                return _image;
            }
            set
            {
                SetProperty(ref _image, value);

            }
        }

        public decimal PointsPerGame
        {
            get
            {
                return _pointsPerGame;
            }
            set
            {
                SetProperty(ref _pointsPerGame, value);
            }
        }

        public string ReboundsPerGame
        {
            get
            {
                return _reboundsPerGame;
            }
            set
            {
                SetProperty(ref _reboundsPerGame, value);
            }
        }

        public string AssistsPerGame
        {
            get
            {
                return _assistsPerGame;
            }
            set
            {
                SetProperty(ref _assistsPerGame, value);
            }
        }

        public string StealsPerGame
        {
            get
            {
                return _stealsPerGame;
            }
            set
            {
                SetProperty(ref _stealsPerGame, value);
            }
        }

        public string BlocksPerGame
        {
            get
            {
                return _blocksPerGame;
            }
            set
            {
                SetProperty(ref _blocksPerGame, value);

            }
        }
        #endregion
    }
}
