using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Moq;
using StudioModel.Domain;
using StudioModel.Dtos.Account;
using StudioService.Services;
using StudioService.Services.Imp;
using Xunit;


/// CHECK HOW TO CONFIGURE SIGN IN MANAGER
namespace StudioBack.Tests
{
    //public class AccountServiceTests
    //{
    //    private AccountService _accountService;
    //    private Mock<SignInManager<UserApp>> _signInManager;
    //    private Mock<UserManager<UserApp>> _userManager;
    //    private Mock<ILogger<AccountService>> _logger;
    //    private Mock<IJwtService> _jwtService;
    //    private Mock<IEmailService> _emailService;

    //    public AccountServiceTests()
    //    {
    //        Setup();
    //        _accountService = new AccountService(_signInManager.Object, _userManager.Object, _logger.Object, _jwtService.Object, _emailService.Object);
    //    }

    //    public void Setup()
    //    {
    //        var mockUserStore = new Mock<IUserStore<UserApp>>();
    //        var mockUserManager = new Mock<UserManager<UserApp>>(mockUserStore.Object);
    //        _signInManager = new Mock<SignInManager<UserApp>>(mockUserManager.Object);
    //        _userManager = mockUserManager;
    //        _logger = new Mock<ILogger<AccountService>>();
    //        _jwtService = new Mock<IJwtService>();
    //        _emailService = new Mock<IEmailService>();

    //        var mockedUserApp = getMockedUserApp();
    //        this._userManager.Setup(sm => sm.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(mockedUserApp);
    //    }


    //    [Fact]
    //    public async Task Login()
    //    {
    //        var mockedDto = new UserLoginDto { Email = "nico@test.com", Password = "1234" };
    //        var result = await _accountService.Login(mockedDto);

    //        result.Should().NotBeNull();
    //    }


    //    private UserApp getMockedUserApp()
    //    {
    //        return new UserApp()
    //        {
    //            Id = "1",
    //            Email = "nico@test.com",
    //        };
    //    }
    //}
}
