using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Project.TakuGames.Model.Business;
using Project.TakuGames.Model.Domain;
using Project.TakuGames.Model.Helpers;
using System.Net;

namespace Proyect.TakuGames.Web.Controllers
{
      /// <summary>
    /// Datos de userController
    /// </summary>   
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]  
    public class UserController : BaseApiController
    {
        private readonly IUserBusiness userBusiness;
        private readonly ICartBusiness cartBusiness;
        
        /// <summary>
        ///  Controller
        /// </summary>
        /// <param name="userBusiness"></param>
        /// <param name="cartBusiness"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param> 
        public UserController(
            IUserBusiness userBusiness,
            ICartBusiness cartBusiness,
            IMapper mapper,
            ILogger<UserController> logger) : base(logger, mapper)
        {
            this.cartBusiness = cartBusiness;
            this.userBusiness = userBusiness;
        }
        /// <summary>
        /// Obtiene el recuento del artículo en el carrito de compras
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>El recuento de artículos en el carrito de compras.</returns>

        [HttpGet("{userId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ComponentError), (int)HttpStatusCode.BadRequest)]
        public ActionResult<int> Get(int userId)
        {
            var response = cartBusiness.GetCartItemCount(userId);
            return response;
        }
        /// <summary>
        /// Verifique la disponibilidad del nombre de usuario
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("validateUserName/{userName}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ComponentError), (int)HttpStatusCode.BadRequest)]
        public ActionResult<bool> ValidateUserName(string userName)
        {
            // var response = userBusiness.CheckUserAwaillabity(userName); 
             var response = userBusiness.isUserExists(userName); 
            return response;
        }
        /// <summary>
        /// Se registra un nuevo usuario
        /// </summary>
        /// <param name="userData"></param>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ComponentError), (int)HttpStatusCode.BadRequest)]
        public ActionResult<int> Post([FromBody] UserMaster userData)
        {
            var response = userBusiness.RegisterUser(userData); 
            return response;
        }
    }
}
