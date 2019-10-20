using NBAStats.Core.Model;

namespace NBAStats.Core.Test.ViewModels.Builders
{
    public class LoginRequestDTOBuilder
    {
        private readonly LoginRequestDTO _request;

        public LoginRequestDTOBuilder()
        {
            _request = new LoginRequestDTO();
        }

        public LoginRequestDTOBuilder WithDefaults()
        {
            _request.Username = "mookiefumi@nba.com";
            _request.Password = "nba@2019";
            _request.AllowSendStats = true;
            _request.UseCacheServices = true;

            return this;
        }

        public LoginRequestDTOBuilder WithUsername(string username)
        {
            _request.Username = username;
            return this;
        }

        public LoginRequestDTO Build()
        {
            return _request;
        }
    }
}
