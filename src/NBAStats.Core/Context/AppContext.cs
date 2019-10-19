namespace NBAStats.Core.Context
{
    public class AppContext
    {
        public AppContext()
        {
            PreferredTeam = "Show all teams";
        }

        public string Email { get; set; }
        public string PreferredTeam { get; set; }
        public bool SortByPointsPerGame { get; set; }
        public string ApiUrl { get; set; }
        public bool UseCacheServices { get; set; }
        public bool AllowSendStats { get; set; }
    }
}
