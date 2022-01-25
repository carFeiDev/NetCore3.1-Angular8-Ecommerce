using Project.TakuGames.Model.Dal;
using Proyect.TakuGames.Test.Helpers;
using Project.TakuGames.Business;
using Project.TakuGames.Model.Domain;
using Project.TakuGames.Model.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Moq;
using Xunit;
using System.Linq;

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
            Assert.Equal(1, resp.UserId);
            Assert.Equal(2, respUser.UserTypeId);
    
        }
    }
}
