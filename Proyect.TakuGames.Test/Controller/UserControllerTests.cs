using AutoMapper;
using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Project.TakuGames.Model.Business;
using Project.TakuGames.Model.Domain;
using Project.TakuGames.Model.ViewModels;
using Proyect.TakuGames.Test.Helpers;
using Proyect.TakuGames.Web.Controllers;


namespace Proyect.TakuGames.Test.Controller
{
    [Collection("Mapper Collection")]
    public class UserControllerTests
    {
        private readonly IMapper mapper;
        private readonly Mock<ILogger<UserController>> logger;
        private readonly Mock<IUserBusiness> _mockUserBusiness;
        private readonly Mock<ICartBusiness> _mockCartBusiness;
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly Mock<IWebHostEnvironment> _mockWebHostEnvironment ;
        public UserControllerTests(MapperFixture mapperFixture)
        {
            mapper = mapperFixture.mapper;
            logger = new Mock<ILogger<UserController>>();
            _mockUserBusiness = new Mock<IUserBusiness>();
            _mockCartBusiness = new Mock<ICartBusiness>();
            _mockConfiguration = new Mock<IConfiguration>();
            _mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
        }

        [Fact(Skip="WebHosEnvironment es null")]
        public void ObtenerCantidadTotalDeItemsTest()
        {
            //arrange
            var user1 = new UserMaster() { UserId = 1, UserName = "lala" };
            _mockCartBusiness.Setup(b => b.GetCartItemCount(user1.UserId)).Returns(2);

            var ctl = new UserController(_mockUserBusiness.Object,_mockCartBusiness.Object, _mockConfiguration.Object, _mockWebHostEnvironment.Object,  mapper, logger.Object);

            //action
            var resp = ctl.GetCartItemNumber(user1.UserId);

            //assert
            var okResult = Assert.IsType<ActionResult<int>>(resp);
            var returnValue = Assert.IsType<int>(okResult.Value);
            Assert.Equal(2, returnValue);
        }

        [Fact(Skip="WebHosEnvironment es null")]
        public void PostRegistrarUsuarioTest()
        {
            //arrange
            var user1 = new UserMaster() { UserId = 2, UserName = "lalali" };
            var uservm = new UserMasterVM() { UserId = 2, UserName = "lalali" };
            _mockUserBusiness.Setup(b => b.RegisterUser(user1)).Returns(user1);

            var ctl = new UserController(_mockUserBusiness.Object,_mockCartBusiness.Object, _mockConfiguration.Object, _mockWebHostEnvironment.Object,  mapper, logger.Object);
            
            //action
            var resp = ctl.Post();

            //assert
            var okResult = Assert.IsType<ActionResult<int>>(resp);
            var returnValue = Assert.IsType<int>(okResult.Value);
            Assert.Equal(1, returnValue);
        }
    }
}
