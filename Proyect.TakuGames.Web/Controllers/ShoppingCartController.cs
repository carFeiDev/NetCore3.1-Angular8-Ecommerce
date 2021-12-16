
using Microsoft.AspNetCore.Mvc;
using Project.TakuGames.Model.Business;
using Project.TakuGames.Model.Dto;
using Project.TakuGames.Model.ViewModels;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Project.TakuGames.Model.Helpers;
using System.Net;

namespace Proyect.TakuGames.Web.Controllers
{
    /// <summary>
    /// Datos de ShoppingCart
    /// </summary>   
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ShoppingCartController : BaseApiController
    {
        private readonly ICartBusiness cartBusiness;
        private readonly IGameBusiness gameBusiness;

        /// <summary>
        ///   Controller
        /// </summary>
        /// <param name="cartBusiness"></param>
        /// <param name="gameBusiness"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param> 
    
        public ShoppingCartController(
            ICartBusiness cartBusiness,
            IGameBusiness gameBusiness,
            IMapper mapper,
            ILogger<ShoppingCartController> logger) : base(logger, mapper)
        {
            this.cartBusiness = cartBusiness;
            this.gameBusiness = gameBusiness;
        }
     
        /// <summary>
        /// Obtiene el carrito de compras para un usuario al iniciar sesión. Si el usuario inicia sesión por primera vez, crea el carrito de compras.
        /// </summary>
        /// <param name="oldUserId"></param>
        /// <param name="newUserId"></param>
        /// <returns>La cantidad de artículos en el carrito de compras.</returns>
        //[Authorize]
        [HttpGet]
        [Route("SetShoppingCart/{oldUserId}/{newUserId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ComponentError), (int)HttpStatusCode.BadRequest)]
        public ActionResult<int> Get(int oldUserId, int newUserId)
        {
            cartBusiness.MergeCart(oldUserId, newUserId);
            var response = cartBusiness.GetCartItemCount(newUserId); 
            return response;
        }

        /// <summary>
        /// Obtiene la lista de artículos en el carrito de compras
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns> 
        [HttpGet]
        [Route("{userId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ComponentError), (int)HttpStatusCode.BadRequest)]
        public ActionResult<List<CartItemDtoVM>> Get(int userId)
        {
            string cartid = cartBusiness.GetCartId(userId);
            var listcart =  gameBusiness.GetGameAvailableInCart(cartid);
            List<CartItemDtoVM> response = _mapper.Map<List<CartItemDto>, List<CartItemDtoVM>>(listcart);
            return response;
        }
           
        /// <summary>
        /// Agrega un solo artículo al carrito de compras. Si el artículo ya existe, aumente la cantidad en uno.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="gameId"></param>
        /// <returns>Recuenta los  artículos del carrito de compras</returns>
        [HttpPost]
        [Route("AddToCart/{userId}/{gameId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ComponentError), (int)HttpStatusCode.BadRequest)]
        public ActionResult<int> Post(int userId, int gameId)
        {
            cartBusiness.AddGameToCart(userId, gameId);
            var response = cartBusiness.GetCartItemCount(userId);
            return response;
        }

        /// <summary>
        /// Reduce la cantidad en uno para un artículo en el carrito de compras.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="gameId"></param>
        /// <returns></returns>   
        [HttpPut("{userId}/{gameId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ComponentError), (int)HttpStatusCode.BadRequest)]
        public ActionResult<int> Put(int userId, int gameId)
        {
            cartBusiness.DeleteOneCardItem(userId, gameId);
            var response = cartBusiness.GetCartItemCount(userId);
            return response;
        }
        /// <summary>
        /// Eliminar un solo artículo del carrito
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="gameId"></param>
        /// <returns></returns>
        [HttpDelete("{userId}/{gameId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ComponentError), (int)HttpStatusCode.BadRequest)]
        public ActionResult<int> Delete(int userId, int gameId)
        {
            cartBusiness.RemoveCartItem(userId, gameId);
            var response = cartBusiness.GetCartItemCount(userId); 
            return response;
        }
        /// <summary>
        /// Limpia el carrito de compras
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpDelete("{userId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ComponentError), (int)HttpStatusCode.BadRequest)]
        public ActionResult<int> Delete(int userId)
        {
            var response = cartBusiness.CleanCart(userId);
            return response;
        }
    }
}
