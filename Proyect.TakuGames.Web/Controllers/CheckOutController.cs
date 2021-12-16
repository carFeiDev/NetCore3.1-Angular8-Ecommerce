
using Microsoft.AspNetCore.Mvc;
using Project.TakuGames.Model.Dto;
using Project.TakuGames.Model.Business;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Proyect.TakuGames.Web.Controllers
{
    /// <summary>
    /// Datos de CheckOut
    /// </summary>   
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class CheckOutController :  BaseApiController
    {
         readonly IOrderBusiness orderBusiness;
         readonly ICartBusiness cartBusiness;

        /// <summary>
        ///  checkOut Controller
        /// </summary>
        /// <param name="orderBusiness"></param>
        /// <param name="cartBusiness"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param> 
        public CheckOutController(
            IOrderBusiness orderBusiness,
            ICartBusiness cartBusiness,
            IMapper mapper,
            ILogger<CheckOutController> logger) : base(logger, mapper)
        {
            this.orderBusiness = orderBusiness;
            this.cartBusiness = cartBusiness;
        }

        /// <summary>
        /// Checkout from shopping cart
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="checkedOutItems"></param>
        /// <returns></returns>
        [HttpPost("{userId}")]
        public int Post(int userId, [FromBody]OrdersDto checkedOutItems)
        {
            orderBusiness.CreateOrder(userId, checkedOutItems);
            return cartBusiness.CleanCart(userId);
        }
    }
}