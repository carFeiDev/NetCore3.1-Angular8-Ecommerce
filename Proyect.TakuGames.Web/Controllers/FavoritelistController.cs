using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using Project.TakuGames.Model.Business;
using Project.TakuGames.Model.Domain;
using Project.TakuGames.Model.ViewModels;
using Project.TakuGames.Model.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AutoMapper;

namespace Proyect.TakuGames.Web.Controllers
{
    /// <summary>
    /// Datos de Favoritos
    /// </summary>   
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class FavoritelistController : BaseApiController
    {
        readonly IFavoritelistBusiness favoritelistBusiness; 

        /// <summary>
        ///  Controller
        /// </summary>
        /// <param name="favoritelistBusiness"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param> 
        public FavoritelistController(
            IFavoritelistBusiness favoritelistBusiness,      
            IMapper mapper,
            ILogger<FavoritelistController> logger) : base(logger, mapper) 
        {
            this.favoritelistBusiness = favoritelistBusiness;    
        } 

        /// <summary>
        /// Obtiene la lista de elementos de Favoritos
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Todos los elementos de Favoritos</returns>       
        [HttpGet("{userId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ComponentError), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<List<GameVM>>> Get(int userId)
        {
            var listFavorite = favoritelistBusiness.GetUserFavoritelist(userId);
            List<GameVM> listFavoriteVM = _mapper.Map<List<Game>, List<GameVM>>(listFavorite);
            return await Task.FromResult(listFavoriteVM).ConfigureAwait(true);
        }

        /// <summary>
        /// Agregar o elimina un item de la lista de favoritos.
        /// Si el elemento no existe, se agregará a la lista de favoritos; de lo contrario, se eliminará.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="gameId"></param>
        /// <returns>Todos los elementos de Favoritos</returns>
         //[Authorize]
        [HttpPost]
        [Route("ToggleFavoritelist/{userId}/{gameId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ComponentError), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<List<GameVM>>> Post(int userId, int gameId)
        {          
            favoritelistBusiness.AddOrDeleteFavoriteListItem(userId, gameId);
            var listFavorite = favoritelistBusiness.GetUserFavoritelist(userId);
            List<GameVM> listFavoriteVm = _mapper.Map<List<Game>, List<GameVM>>(listFavorite);
            return await Task.FromResult(listFavoriteVm).ConfigureAwait(true);
        }

        /// <summary>
        /// Limpia favoritos
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        // [Authorize]
        [HttpDelete("{userId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ComponentError), (int)HttpStatusCode.BadRequest)]
        public ActionResult<int> Delete(int userId)
        {
            var response = favoritelistBusiness.ClearFavoritelist(userId);
            return Ok(response);
        }
    }
}
