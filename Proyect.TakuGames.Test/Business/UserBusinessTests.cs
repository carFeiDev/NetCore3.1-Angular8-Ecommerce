using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using Project.TakuGames.Model.Dal;
using Proyect.TakuGames.Test.Helpers;
using Project.TakuGames.Business;
using Project.TakuGames.Model.Domain;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Project.TakuGames.Model.Helpers;



namespace Proyect.TakuGames.Test.Business
{
    [Collection("Mapper Collection")]
    public class UserBusinessTests
    {
        private readonly IMapper mapper;
        private readonly Mock<ILogger<UserBusiness>> logger;
        private readonly IUnitOfWork uow;
        private readonly Mock<IConfiguration> mockConfig;
        
        public UserBusinessTests(MapperFixture mapperFixture)
        {
            mapper = mapperFixture.mapper;
            logger = new Mock<ILogger<UserBusiness>>();
            uow = new FakeDAL.FakeUnitOfWork();
            mockConfig = new Mock<IConfiguration>();
           
        }
        
        [Fact]
        public void RegistroDeUsuarioSatisfactorioTest()
        {
            //arrange
            var user = new UserMaster { UserId = 1, UserName = "admin", FirstName = "firstname", LastName = "lastname", Password = Encrypt.GetSHA256("1234") };
        
            //action
            var userbusiness = new UserBusiness(uow, mapper, logger.Object, mockConfig.Object);
            var resp = userbusiness.RegisterUser(user);
            var respUser = uow.UserMasterRepository.Get().Where(x => x.UserId == user.UserId).FirstOrDefault();
            
            //assert
            Assert.Equal(1, resp);
            Assert.Equal(2, respUser.UserTypeId);
    
        }
        [Fact]
        public void ChekearUserDisponibleTest()
        {
            //arrange
            var user = new UserMaster { UserId = 1, UserName = "admin", FirstName = "firstname", LastName = "lastname" };
            uow.UserMasterRepository.Insert(user);
            
            var userbusiness = new UserBusiness(uow, mapper, logger.Object, mockConfig.Object);

            //action
            var resp = userbusiness.CheckUserAwaillabity(user.UserName);

            //assert
            Assert.True(resp);

        }
        [Fact]
        public void ChekearUserNoDisponibleTest()
        {
            //arrange
            var user = new UserMaster { UserId = 1, UserName = "admin", FirstName = "firstname", LastName = "lastname" };

            //action
            var userbusiness = new UserBusiness(uow, mapper, logger.Object,mockConfig.Object);
            var resp = userbusiness.CheckUserAwaillabity(user.UserName);

            //assert
            Assert.False(resp);

        }
    }
}
