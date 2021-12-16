
using System.Collections.Generic;
using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Project.TakuGames.Model.Domain;
using Project.TakuGames.Model.Helpers;
using Project.TakuGames.Model.ViewModels;

namespace Proyect.TakuGames.Web.Controllers
{
       /// <summary>
        /// Obtiene los datos de los games
        /// </summary>
        /// <returns>lista de games</returns>
        /// <response code="200">Devuelve la lista de games</response>
        /// <response code="400">No ha pasado las validaciones</response>    
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : BaseApiController
    {
       
        private readonly Project.TakuGames.Model.Business.IGamesBusiness gamesBussines;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="gamesBussines"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        public GamesController(Project.TakuGames.Model.Business.IGamesBusiness gamesBussines, IMapper mapper,
          
             ILogger<GamesController> logger) : base(logger,mapper)
        {
            this.gamesBussines = gamesBussines;
            
        }

        /// <summary>
        /// Obtiene los datos de los games
        /// </summary>
        /// <returns>lista de games</returns>
        /// <response code="200">Devuelve la lista de games</response>
        /// <response code="400">No ha pasado las validaciones</response>    
        [HttpGet]
        [ProducesResponseType(typeof(List<GamesVM>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ComponentError), (int)HttpStatusCode.BadRequest)]
        public IActionResult Get()
        {
            var resp = gamesBussines.Listar();
            List<GamesVM> response = _mapper.Map<List<Games>, List<GamesVM>>(resp);
            return Ok(response);
        }

   /// <summary>
        /// Obtiene los datos de los games
        /// </summary>
        /// <returns>lista de games</returns>
        /// <response code="200">Devuelve la lista de games</response>
        /// <response code="400">No ha pasado las validaciones</response>    
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(GamesVM), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ComponentError), (int)HttpStatusCode.BadRequest)]
        public IActionResult Get(int id)
        {
            var gam = gamesBussines.Obtener(id);
            if (gam == null)
            {
                return NotFound();
            }
            var response = _mapper.Map<Games, GamesVM>(gam);
            return Ok(response);
        }

        /// <summary>
        /// Crea  un game
        /// </summary>
        /// <returns>game creado</returns>
        /// <response code="201">path al game</response>
        /// <response code="400">No ha pasado las validaciones</response>    
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ComponentError), (int)HttpStatusCode.BadRequest)]
        public IActionResult Post(GamesVM game)
        {
            var gam = _mapper.Map<GamesVM, Games>(game);
            var resp = gamesBussines.Crear(gam);
            GamesVM response = _mapper.Map<Games, GamesVM>(resp);
            return Created($"{Request.Path}/{response.GameId}", response);
        }
        /// <summary>
        /// Modifica un game en la app
        /// </summary>
        /// <returns>game modificado</returns>
        /// <response code="200">game modificado</response>
        /// <response code="400">No ha pasado las validaciones</response>  
        /// <response code="404">No se encontró al Juego</response>    
        [HttpPost("{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ComponentError), (int)HttpStatusCode.BadRequest)]
        public IActionResult Post(int id, GamesVM game)
        {
            var gam = _mapper.Map<GamesVM, Games>(game);
            gam.GameId = id;
            var resp = gamesBussines.Modificar(gam);
            GamesVM response = _mapper.Map<Games, GamesVM>(resp);
            return Ok(response);
        }
           /// <summary>
        /// Obtiene los datos de los games
        /// </summary>
        /// <returns>lista de games</returns>
        /// <response code="200">Devuelve la lista de games</response>
        /// <response code="400">No ha pasado las validaciones</response>    

        [HttpPut("{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ComponentError), (int)HttpStatusCode.BadRequest)]
        public IActionResult Put(GamesVM game)
        {
            var gam = _mapper.Map<GamesVM, Games>(game);
            var resp = gamesBussines.Modificar(gam);
            GamesVM response = _mapper.Map<Games, GamesVM>(resp);
            return Ok(response);
        }
           /// <summary>
        /// Obtiene los datos de los games
        /// </summary>
        /// <returns>lista de games</returns>
        /// <response code="200">Devuelve la lista de games</response>
        /// <response code="400">No ha pasado las validaciones</response>    
        [HttpDelete("{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ComponentError), (int)HttpStatusCode.BadRequest)]
        public void Delete(int id)
        {
            gamesBussines.Eliminar(id);
            
        }
    }
}

