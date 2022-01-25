using Project.TakuGames.Model.Dal;
using Proyect.TakuGames.Test.Helpers;
using Project.TakuGames.Business;
using Project.TakuGames.Model.Domain;
using Project.TakuGames.Model.Business;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using System;
using System.Linq;
using AutoMapper;

namespace Proyect.TakuGames.Test.Business
{
    [Collection("Mapper Collection")]
    public class FavoritelistBusinessTests
    {
        private readonly IMapper mapper;
        private readonly Mock<ILogger<FavoritelistBusiness>> mocklogger;
        private readonly IUnitOfWork unitOfWork;
        private readonly Mock<IGameBusiness> mockGameBusiness;
        private readonly Mock<IUserBusiness> mockUserBusiness;
        public FavoritelistBusinessTests(MapperFixture mapperFixture)
        {
            mapper = mapperFixture.mapper;
            mocklogger = new Mock<ILogger<FavoritelistBusiness>>();
            unitOfWork = new FakeDAL.FakeUnitOfWork();
            mockGameBusiness = new Mock<IGameBusiness>();
            mockUserBusiness = new Mock<IUserBusiness>();

        }
        [Fact]
        public void EliminarTodosLositemsDeFavoritosTest()
        {
            //arrange
            var user1= new UserMaster{ UserId = 1, FirstName = "firsname1", LastName= "lastname1", UserName= "username1", Password = "password1", Gender = "gender1", UserTypeId = 2};
            var favoritelist1 = new Favoritelist() {FavoritelistId = "1", UserId = 1, DateCreated = DateTime.Now.Date};         
            var favoritelistItem1 = new FavoritelistItems(){ FavoritelistItemId = 1, FavoritelistId = "1", ProductId = 1 };
            var favoritelistItem2 = new FavoritelistItems(){ FavoritelistItemId = 2, FavoritelistId = "1", ProductId = 2 };
            var favoritelistItem3 = new FavoritelistItems(){ FavoritelistItemId = 3, FavoritelistId = "1", ProductId = 3 };
            var favoritelistItem4 = new FavoritelistItems(){ FavoritelistItemId = 4, FavoritelistId = "1", ProductId = 4 };

            unitOfWork.UserMasterRepository.Insert(user1);
            unitOfWork.FavoritelistRepository.Insert(favoritelist1);
            unitOfWork.FavoritelistItemsRepository.Insert(favoritelistItem1);
            unitOfWork.FavoritelistItemsRepository.Insert(favoritelistItem2);
            unitOfWork.FavoritelistItemsRepository.Insert(favoritelistItem3);
            unitOfWork.FavoritelistItemsRepository.Insert(favoritelistItem4);   
            
            var favoritelistBusiness = new FavoritelistBusiness(unitOfWork, mapper,mockGameBusiness.Object,mockUserBusiness.Object, mocklogger.Object);
            
            //action    
            var resp = favoritelistBusiness.ClearFavoritelist(user1.UserId);
            var list = unitOfWork.FavoritelistItemsRepository.Get().Where(x =>  x.FavoritelistId == favoritelist1.FavoritelistId).ToList();
            
            //assert   
            Assert.Empty(list);
   
        }
        [Fact]
        public void EliminarItemDeFavoritosTest()
        {
            //arrange
            var user1= new UserMaster{ UserId = 1, FirstName = "firsname1", LastName= "lastname1", UserName= "username1", Password = "password1", Gender = "gender1", UserTypeId = 2};
            var favoritelist1 = new Favoritelist() {FavoritelistId = "1", UserId = 1, DateCreated = DateTime.Now.Date};
            var favoritelistItem1 = new FavoritelistItems(){ FavoritelistItemId = 1, FavoritelistId = "1", ProductId = 1 };

            unitOfWork.UserMasterRepository.Insert(user1);
            unitOfWork.FavoritelistRepository.Insert(favoritelist1);
            unitOfWork.FavoritelistItemsRepository.Insert(favoritelistItem1);   
            
            var favoritelistBusiness = new FavoritelistBusiness(unitOfWork, mapper,mockGameBusiness.Object,mockUserBusiness.Object, mocklogger.Object);
            
            //action    
            favoritelistBusiness.ToggleFavoritelistItem(user1.UserId, favoritelistItem1.ProductId);

            var result = unitOfWork.FavoritelistItemsRepository.Get().Where(x => x.ProductId == favoritelistItem1.ProductId && x.FavoritelistId == favoritelist1.FavoritelistId).FirstOrDefault();
            //assert
            Assert.Null(result);

        }

        [Fact]
        public void AgregarItemDeFavoritosTest()
        {
            //arrange
            var user1= new UserMaster{ UserId = 1, FirstName = "firsname1", LastName= "lastname1", UserName= "username1", Password = "password1", Gender = "gender1", UserTypeId = 2};
            var favoritelist1 = new Favoritelist() {FavoritelistId = "1", UserId = 1, DateCreated = DateTime.Now.Date}; 
            var favoritelistItem1 = new FavoritelistItems(){ FavoritelistItemId = 1, FavoritelistId = "1", ProductId = 1 };

            unitOfWork.UserMasterRepository.Insert(user1);
            unitOfWork.FavoritelistRepository.Insert(favoritelist1);
           
            var favoritelistBusiness = new FavoritelistBusiness(unitOfWork, mapper,mockGameBusiness.Object,mockUserBusiness.Object, mocklogger.Object);
            
            //action    
            favoritelistBusiness.ToggleFavoritelistItem(user1.UserId, favoritelistItem1.ProductId);

            var result = unitOfWork.FavoritelistItemsRepository.Get().Where(x => x.ProductId == favoritelistItem1.ProductId && x.FavoritelistId == favoritelist1.FavoritelistId).FirstOrDefault();
            //assert
            Assert.NotNull(result);     

        }
        
        [Fact]
        public void ObtenerElIdDeFavoritosTest()
        {
            //arrange
            var favoritelist1 = new Favoritelist() {FavoritelistId = "1", UserId = 1, DateCreated = DateTime.Now.Date};
            var user1= new UserMaster{ UserId = 1, FirstName = "firsname1", LastName= "lastname1", UserName= "username1", Password = "password1", Gender = "gender1",UserTypeId = 2};
            
            unitOfWork.FavoritelistRepository.Insert(favoritelist1);
            unitOfWork.UserMasterRepository.Insert(user1);

            //action    
            var favoritelistBusiness = new FavoritelistBusiness(unitOfWork, mapper,mockGameBusiness.Object,mockUserBusiness.Object, mocklogger.Object);

            var resp = favoritelistBusiness.GetFavoritelistId(user1.UserId);
            
            //assert
            Assert.Equal(favoritelist1.FavoritelistId,resp);         
        }    
        [Fact]
        public void ObtenerIdCreandoUnNuevoFavoritoTest()
        {
            //arrange
            var favoritelist1 = new Favoritelist() {FavoritelistId = "1", UserId = 1, DateCreated = DateTime.Now.Date};
            var user1= new UserMaster{ UserId = 2, FirstName = "firsname1", LastName= "lastname1", UserName= "username1", Password = "password1", Gender = "gender1",UserTypeId = 2};
            
            unitOfWork.FavoritelistRepository.Insert(favoritelist1);
            unitOfWork.UserMasterRepository.Insert(user1);

            //action    
            var favoritelistBusiness = new FavoritelistBusiness(unitOfWork, mapper,mockGameBusiness.Object,mockUserBusiness.Object, mocklogger.Object);

            var resp= favoritelistBusiness.GetFavoritelistId(user1.UserId);
            var favoritelist = unitOfWork.FavoritelistRepository.Get().Where(x => x.FavoritelistId == resp).FirstOrDefault();
            
            //assert
            Assert.IsType<String>(resp);
            Assert.Equal(resp,favoritelist.FavoritelistId);
            
        }
    }
}