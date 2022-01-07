using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Project.TakuGames.Model.Business;
using Project.TakuGames.Model.Domain;
using Proyect.TakuGames.Test.Helpers;
using Proyect.TakuGames.Web.Controllers;
using Xunit;

namespace Proyect.TakuGames.Test.Controller
{
    [Collection("Mapper Collection")]

    public class UserControllerTests
    {
        private readonly IMapper mapper;
        private readonly Mock<ILogger<UserController>> logger;
        private readonly Mock<IUserBusiness> _mockUserBusiness;
        private readonly Mock<ICartBusiness> _mockCartBusiness;

        public UserControllerTests(MapperFixture mapperFixture)
        {
            mapper = mapperFixture.mapper;
            logger = new Mock<ILogger<UserController>>();
            _mockUserBusiness = new Mock<IUserBusiness>();
            _mockCartBusiness = new Mock<ICartBusiness>();

        }
        [Fact]
        public void ObtenerCantidadTotalDeItemsTest()
        {
            //arrange
            var user1 = new UserMaster() { UserId = 1, UserName = "lala" };
            _mockCartBusiness.Setup(b => b.GetCartItemCount(user1.UserId)).Returns(2);

            var ctl = new UserController(_mockUserBusiness.Object, _mockCartBusiness.Object, mapper, logger.Object);

            //action
            var resp = ctl.Get(user1.UserId);

            //assert
            var okResult = Assert.IsType<ActionResult<int>>(resp);
            var returnValue = Assert.IsType<int>(okResult.Value);
            Assert.Equal(2, returnValue);
        }
        [Fact]
        public void RevisarSiElusuarioEstaDisponibleTrueTest()
        {
            //arrange
            var user1 = new UserMaster() { UserId = 1, UserName = "lala" };
            _mockUserBusiness.Setup(b => b.isUserExists(user1.UserName)).Returns(true);

            var ctl = new UserController(_mockUserBusiness.Object, _mockCartBusiness.Object, mapper, logger.Object);

            //action
            var resp = ctl.ValidateUserName(user1.UserName);

            //assert
            var okResult = Assert.IsType<ActionResult<bool>>(resp);
            var returnValue = Assert.IsType<bool>(okResult.Value);
            Assert.True(returnValue);
        }
        [Fact]
        public void RevisarSiElusuarioEstaDisponibleNoTrueTest()
        {
            //arrange
            var user1 = new UserMaster() { UserId = 1, UserName = "lala" };
            _mockUserBusiness.Setup(b => b.isUserExists(user1.UserName)).Returns(false);

            var ctl = new UserController(_mockUserBusiness.Object, _mockCartBusiness.Object, mapper, logger.Object);

            //action
            var resp = ctl.ValidateUserName(user1.UserName);

            //assert
            var okResult = Assert.IsType<ActionResult<bool>>(resp);
            var returnValue = Assert.IsType<bool>(okResult.Value);
            Assert.False(returnValue);
        }
        // [Fact(Skip = "testear")]
        [Fact]
        public void PostRegistrarUsuarioTest()
        {
            //arrange
            var user1 = new UserMaster() { UserId = 1, UserName = "lala" };
            _mockUserBusiness.Setup(b => b.RegisterUser(user1)).Returns(1);

            var ctl = new UserController(_mockUserBusiness.Object, _mockCartBusiness.Object, mapper, logger.Object);

            //action
            var resp = ctl.Post(user1);

            //assert
            var okResult = Assert.IsType<ActionResult<int>>(resp);
            var returnValue = Assert.IsType<int>(okResult.Value);
            Assert.Equal(1, returnValue);


        }
    }
}
