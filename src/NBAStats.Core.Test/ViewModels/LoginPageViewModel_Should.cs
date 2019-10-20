using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NBAStats.Core.Model;
using NBAStats.Core.Services;
using NBAStats.Core.Test.ViewModels.Base;
using NBAStats.Core.Test.ViewModels.Builders;
using NBAStats.Core.ViewModels;
using NBAStats.Core.Views;
using Xunit;

namespace NBAStats.Core.Test.ViewModels
{
    public class LoginPageViewModel_Should : BaseViewModelTest
    {
        private readonly Mock<ILoginService> _loginService;
        private readonly LoginRequestDTO _loginRequest;

        //sut = subject under test
        private readonly LoginPageViewModel _sut;

        public LoginPageViewModel_Should()
        {
            _loginRequest = new LoginRequestDTOBuilder()
                .WithDefaults()
                .Build();

            _loginService = new Mock<ILoginService>();

            _loginService.Setup(m => m.Login(It.Is<LoginRequestDTO>(p => p.Username == _loginRequest.Username))).ReturnsAsync(() =>
               {
                   return new LoginResponseDTO
                   {
                       IsValid = true,
                       ApiUrl = "google.es"
                   };
               });
            _loginService.Setup(m => m.Login(It.Is<LoginRequestDTO>(p => p.Username.Contains("aaa")))).ReturnsAsync(() =>
            {
                return new LoginResponseDTO
                {
                    IsValid = false
                };
            });

            _sut = new LoginPageViewModel(CoreServices, _loginService.Object);
        }

        [Fact]
        public void Execute_LoginCommand_If_Username_And_Password_Are_Not_Empty_And_Null()
        {
            _sut.Username = _loginRequest.Username;
            _sut.Password = _loginRequest.Password;

            var result = _sut.LoginCommand.CanExecute(null);

            result.Should().BeTrue();
        }

        [Theory]
        [InlineData("", "anyValue")]
        [InlineData("anyValue", "")]
        [InlineData(null, "anyValue")]
        [InlineData("anyValue", null)]
        [InlineData("", "")]
        [InlineData(null, null)]
        public void No_Execute_LoginCommand_If_Username_Or_Password_Are_Null_Or_Empty(string username, string password)
        {
            _sut.Username = username;
            _sut.Password = password;

            var result = _sut.LoginCommand.CanExecute(null);

            result.Should().BeFalse();
        }

        [Fact]
        public async Task Call_LoginService_Login_Method_With_Properly_Params()
        {
            _sut.Username = _loginRequest.Username;
            _sut.Password = _loginRequest.Password;
            _sut.UseCacheServices = _loginRequest.UseCacheServices;
            _sut.AllowSendStats = _loginRequest.AllowSendStats;

            await _sut.LoginCommand.ExecuteAsync();

            _loginService.Verify(m => m.Login(It.Is<LoginRequestDTO>(p =>
                p.Username == _sut.Username &&
                p.Password == _sut.Password &&
                p.UseCacheServices == _sut.UseCacheServices &&
                p.AllowSendStats == _sut.AllowSendStats)), times: Times.Once);
        }

        [Fact]
        public async Task NavigateTo_MyTabbedPage_If_Login_Is_Valid()
        {
            _sut.Username = _loginRequest.Username;
            _sut.Password = _loginRequest.Password;
            _sut.UseCacheServices = _loginRequest.UseCacheServices;
            _sut.AllowSendStats = _loginRequest.AllowSendStats;

            await _sut.LoginCommand.ExecuteAsync();

            NavigationService.Verify(m => m.NavigateAsync($"/{nameof(MyTabbedPage)}"), times: Times.Once);
        }

        [Fact]
        public async Task Show_Toast_If_Login_Is_NotValid()
        {
            _sut.Username = "aaanother@username";
            _sut.Password = _loginRequest.Password;

            await _sut.LoginCommand.ExecuteAsync();

            UserDialogs.Verify(m => m.Toast("Invalid login, please try it again", null), Times.Once);
        }

        [Fact]
        public async Task Show_Toast_If_Exception_Raise_While_Login()
        {
            _sut.Username = "another@username";
            _sut.Password = _loginRequest.Password;

            await _sut.LoginCommand.ExecuteAsync();

            UserDialogs.Verify(m => m.Toast("There was an error calling the API", null), Times.Once);
        }
    }
}
