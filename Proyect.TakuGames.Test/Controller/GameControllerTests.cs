using Xunit;
using Moq;
using AutoMapper;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Project.TakuGames.Model.Business;
using Project.TakuGames.Model.Domain;
using Project.TakuGames.Model.ViewModels;
using Proyect.TakuGames.Test.Helpers;
using Proyect.TakuGames.Web.Controllers;

namespace Proyect.TakuGames.Test.Controller
{
    [Collection("Mapper Collection")]
    public class GameControllerTests
    {
        private readonly IMapper mapper;
        private readonly Mock<ILogger<GameController>> logger;
        private readonly Mock<IGameBusiness> mockGameBusiness;
        private readonly Mock<IConfiguration> mockConfiguration;
        private readonly Mock<IWebHostEnvironment> mockIWebHostEnvironment ;
        readonly string coverImageFolderPath = string.Empty;        
        public GameControllerTests(MapperFixture mapperFixture)
        {
            
             mapper = mapperFixture.mapper;
             logger = new Mock<ILogger<GameController>>();
             mockGameBusiness = new Mock<IGameBusiness>();
             mockConfiguration = new Mock<IConfiguration>();
             mockIWebHostEnvironment = new Mock<IWebHostEnvironment>();
        }

         [Fact (Skip = "WebHostEnvironment es null")]    
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

            var ctl = new GameController(mockGameBusiness.Object, mockConfiguration.Object, mockIWebHostEnvironment.Object, mapper, logger.Object);
            //action
            var resp = ctl.Get();

            //assert
            var okResult =  Assert.IsType<ActionResult<List<GameVM>>>(resp);
            var returnValue = Assert.IsType<List<GameVM>>(okResult.Value);
            Assert.Single(returnValue);
        }
    }
}










