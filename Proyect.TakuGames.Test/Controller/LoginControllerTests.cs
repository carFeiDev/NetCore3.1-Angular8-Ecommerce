using AutoMapper;
using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Project.TakuGames.Model.Business;
using Project.TakuGames.Model.Domain;
using Proyect.TakuGames.Test.Helpers;
using Proyect.TakuGames.Web.Controllers;



namespace Proyect.TakuGames.Test.Controller
{
    [Collection("Mapper Collection")]     
    public class LoginControllerTests
    {
        private readonly Mock<IUserBusiness> mockUserBusiness;
        private readonly IMapper mapper;
        private readonly Mock<ILogger<LoginController>> logger;

        public LoginControllerTests(MapperFixture mapperFixture)
        {
            mapper = mapperFixture.mapper;
            logger = new Mock<ILogger<LoginController>>();
            mockUserBusiness = new Mock<IUserBusiness>();    
        }

        [Fact]
        public void  LoginTest()
        {
            //Arrange
            var user = new UserMaster() { UserId = 1 };
            mockUserBusiness.Setup(b => b.AuthenticateUser(user)).Returns(user).Verifiable();
            var ctl = new LoginController(mockUserBusiness.Object, mapper, logger.Object);

            //Action
            var result = ctl.Login(user);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
            mockUserBusiness.VerifyAll();
        }
    }

}