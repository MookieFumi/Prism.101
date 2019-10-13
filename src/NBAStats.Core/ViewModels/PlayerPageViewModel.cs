using NBAStats.Core.Model;
using NBAStats.Core.ViewModels.Base;
using Prism.Navigation;

namespace NBAStats.Core.ViewModels
{
    public class PlayerPageViewModel : BaseViewModel, IInitialize
    {
        public PlayerPageViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        private string _name;

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

        private string _minutesPerGame;

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

        private string _image;

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

        private string _pointsPerGame;

        public string PointsPerGame
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

        private string _reboundsPerGame;

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

        private string _assistsPerGame;

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

        private string _stealsPerGame;

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

        private string _blocksPerGame;

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

        public void Initialize(INavigationParameters parameters)
        {
            var player = parameters.GetValue<PlayerDTO>("player");

            Name = player.Name;
            MinutesPerGame = player.MinutesPerGame;
            Image = player.Image;
            PointsPerGame = player.PointsPerGame;
            ReboundsPerGame = player.ReboundsPerGame;
            AssistsPerGame = player.AssistsPerGame;
            StealsPerGame = player.StealsPerGame;
            BlocksPerGame = player.BlocksPerGame;
        }
    }
}
