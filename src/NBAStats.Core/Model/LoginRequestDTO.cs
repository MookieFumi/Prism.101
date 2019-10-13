namespace NBAStats.Core.Model
{
    public class LoginRequestDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool AllowSendStats { get; set; }
        public bool UseCacheServices { get; set; }
    }
}
