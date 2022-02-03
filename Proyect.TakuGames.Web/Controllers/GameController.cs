using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Project.TakuGames.Model.Business;
using Project.TakuGames.Model.Domain;
using Project.TakuGames.Model.Helpers;
using Project.TakuGames.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoMapper;
using Newtonsoft.Json;
namespace Proyect.TakuGames.Web.Controllers
{
    /// <summary>
    /// Datos de los juegos
    /// </summary>   
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class GameController : BaseApiController
    {
        readonly IGameBusiness gameBusiness;
        readonly IWebHostEnvironment hostingEnvironment;
        readonly IConfiguration config;
        readonly string coverImageFolderPath = string.Empty;

        /// <summary>
        /// Game Controller
        /// </summary>
        /// <param name="gameBusiness"></param>
        /// <param name="config"></param>
        /// <param name="hostEnvironment"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>    
        public GameController(
            IGameBusiness gameBusiness,
            IConfiguration config,
            IWebHostEnvironment hostEnvironment,
            IMapper mapper,
            ILogger<GameController> logger) : base(logger, mapper)
        {
            this.config = config;
            this.gameBusiness = gameBusiness;
            this.hostingEnvironment = hostEnvironment;
            this.coverImageFolderPath = Path.Combine(hostingEnvironment.WebRootPath, "Upload");
            if (!Directory.Exists(coverImageFolderPath))
            {
                Directory.CreateDirectory(coverImageFolderPath);
            }
        }

        /// <summary>
        /// Obtiene los datos de los juegos
        /// </summary>
        /// <returns>lista de games</returns>
        /// <response code="200">Devuelve la lista de juegos</response>
        /// <response code="400">No ha pasado las validaciones</response>    
        [HttpGet]
        [ProducesResponseType(typeof(List<GameVM>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ComponentError), (int)HttpStatusCode.BadRequest)]
        public ActionResult<List<GameVM>> Get()
        {
            var resp = gameBusiness.GetAllGames();
            List<GameVM> response = _mapper.Map<List<Game>, List<GameVM>>(resp);
            return response;
        }
         /// <summary>
        /// Obtiene los datos del juego
        /// </summary>
        /// <returns>Datos del juego</returns>
        /// <response code="200">Datos del juego</response>
        /// <response code="400">No ha pasado las validaciones</response>    
        /// <response code="404">No se encontró al juego</response>    
        [HttpGet("{id:int}", Name = "Get")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ComponentError), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public ActionResult<GameVM> Get(int id)
        {
           var resp = gameBusiness.GetGame(id);   
           if (resp == null )
            {
                return NotFound();
            }
            GameVM response = _mapper.Map<Game, GameVM>(resp);
            return response;
        }

        /// <summary>
        /// Crea un juego en la app
        /// </summary>
        /// <returns>Juego creado</returns>
        /// <response code="201">path al juego</response>
        /// <response code="400">No ha pasado las validaciones</response>        
        [HttpPost, DisableRequestSizeLimit]
        //[Authorize(Policy = UserRoles.Admin)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ComponentError), (int)HttpStatusCode.BadRequest)]
        public ActionResult<GameVM> Post()
        {
            GameVM gameVm = JsonConvert.DeserializeObject<GameVM>(Request.Form["gameFormData"].ToString());
            GameVM gameWithImageVm = UploadImage(gameVm);
            Game game = _mapper.Map<GameVM,Game>(gameWithImageVm);
            Game resp = gameBusiness.CreateGame(game);
            GameVM response = _mapper.Map<Game, GameVM>(resp);
            return  Created($"{Request.Path}/{response.GameId}", response);
        }

         /// <summary>
        /// Modifica un juego en la app
        /// </summary>
        /// <returns>Juego modificado</returns>
        /// <response code="200">juego modificado</response>
        /// <response code="400">No ha pasado las validaciones</response>  
        /// <response code="404">No se encontró  el juego</response>  
        [HttpPut("{Id:int}")]
        //[Authorize(Policy = UserRoles.Admin)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ComponentError), (int)HttpStatusCode.BadRequest)]
        public ActionResult<GameVM> Put()
        {
            GameVM gameEditedVm = JsonConvert.DeserializeObject<GameVM>(Request.Form["gameFormData"].ToString());
            GameVM gameEditedWithImageVm = UploadImage(gameEditedVm);
            Game gameEdited = _mapper.Map<GameVM,Game>(gameEditedWithImageVm);
            Game resp = gameBusiness.UpdateGame(gameEdited);
            GameVM response = _mapper.Map<Game, GameVM>(resp);
            return response;
        }
        /// <summary>
        /// Obtiene  los generos
        /// </summary>
        /// <returns>lista de generos</returns>
        /// <response code="200">Devuelve la lista de  generos</response>
        /// <response code="400">No ha pasado las validaciones</response>    
        [HttpGet("GetCategoriesList")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ComponentError), (int)HttpStatusCode.BadRequest)]
        public ActionResult<List<CategoriesVM>> CategoriesList()
        {
            var resp = gameBusiness.GetCategories();
            List<CategoriesVM> response = _mapper.Map<List<Categories>, List<CategoriesVM>>(resp);
            return response;

        }
        /// <summary>
        /// Obtiene  juegos similares
        /// </summary>
        /// <returns>lista de juegos similares</returns>
        /// <response code="200">Devuelve la lista de  juegos similares</response>
        /// <response code="400">No ha pasado las validaciones</response>    
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ComponentError), (int)HttpStatusCode.BadRequest)]
        [Route("GetSimilarGames/{gameId}")]
        public async Task<ActionResult<List<GameVM>>> SimilarGames(int gameId)
        {
            var resp = gameBusiness.GetSimilarGames(gameId);
            List<GameVM> response = _mapper.Map<List<Game>, List<GameVM>>(resp);
            return await Task.FromResult(response).ConfigureAwait(true);
        }
        /// <summary>
        /// Elimina un juego en la app
        /// </summary>
        /// <returns>Juego eliminado</returns>
        /// <response code="200">juego eliminado</response>
        /// <response code="400">No ha pasado las validaciones</response>  
        /// <response code="404">No se encontró  el juego</response>  
        [HttpDelete("{id}")]
        //[Authorize(Policy = UserRoles.Admin)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ComponentError), (int)HttpStatusCode.BadRequest)]
        public ActionResult<int> Delete(int id)
        {
            string coverFileName = gameBusiness.DeleteGame(id);
            if (coverFileName != config["DefaultCoverImageFile"])
            {
                string fullPath = Path.Combine(coverImageFolderPath, coverFileName);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
            }
            return 1;
        }
        private GameVM UploadImage(GameVM gameVm)
        { 
            if (Request.Form.Files.Count > 0)
            {
                var file = Request.Form.Files[0];
                if (file.Length > 0)
                {
                    string fileName = Guid.NewGuid() + ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    string fullPath = Path.Combine(coverImageFolderPath, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    gameVm.CoverFileName = fileName;
                }
            }
            else
            {
                gameVm.CoverFileName = config["DefaultCoverImageFile"];
            }

            return gameVm;
        }  
    }
}
