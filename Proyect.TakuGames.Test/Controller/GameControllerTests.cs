
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Project.TakuGames.Model.Business;
using Project.TakuGames.Model.Domain;
using Project.TakuGames.Model.ViewModels;
using Proyect.TakuGames.Test.Helpers;
using Proyect.TakuGames.Web.Controllers;
using System.Collections.Generic;
using Xunit;

namespace Proyect.TakuGames.Test.Controller
{
    [Collection("Mapper Collection")]
    public class GameControllerTests
    {
        private readonly IMapper mapper;
        private readonly Mock<ILogger<GameController>> logger;
        private readonly Mock<IGameBusiness> mockGameBusiness;
        private readonly Mock<IConfiguration> mockConfiguration;
        private readonly Mock<IWebHostEnvironment> mockhostingEnvironment ;
        readonly string coverImageFolderPath = string.Empty;
        
        public GameControllerTests(MapperFixture mapperFixture)
        {
            
             mapper = mapperFixture.mapper;
             logger = new Mock<ILogger<GameController>>();
             mockGameBusiness = new Mock<IGameBusiness>();
             mockConfiguration = new Mock<IConfiguration>();
             mockhostingEnvironment = new Mock<IWebHostEnvironment>();
        }

         [Fact(Skip = "Testear: Error en el GameController")]    
        public void ObtenerListaTest()
        {
          //arrange   
            var game1 = new Game() {
                 GameId = 1,
                 Title = "title",
                 Description = "dsda",
                 Developer = "dsddsdsa",
                 Publisher = "dsda",
                 Platform = "dsda",
                 Category = "dsda",
                 Price = 1200,
                 CoverFileName = "Dsadas"
           };
            mockGameBusiness.Setup(b => b.GetAllGames()).Returns(new List<Game>() { game1 });

            var ctl = new GameController(mockGameBusiness.Object, mockConfiguration.Object, mockhostingEnvironment.Object, mapper, logger.Object);
            //action
            var resp = ctl.Get();

            //assert
            var okResult =  Assert.IsType<ActionResult<List<GameVM>>>(resp);
            var returnValue = Assert.IsType<List<GameVM>>(okResult.Value);
            Assert.Single(returnValue);
        }
    }
}










