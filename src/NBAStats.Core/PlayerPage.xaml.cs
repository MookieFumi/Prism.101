using NBAStats.Core.Model;
using Xamarin.Forms;

namespace NBAStats.Core
{
    public partial class PlayerPage : ContentPage
    {
        public PlayerPage(PlayerDTO player)
        {
            InitializeComponent();
            Title = player.Name;
            BindingContext = player;
        }
    }
}
