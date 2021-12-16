using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Project.TakuGames.Model.Business;
using Project.TakuGames.Model.Dto;
using Project.TakuGames.Model.Helpers;
using Project.TakuGames.Model.ViewModels;
using System.Collections.Generic;
using System.Net;

namespace Proyect.TakuGames.Web.Controllers
{
    /// <summary>
    /// Datos de customerOrder
    /// </summary>   
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]    
    public class CustomerOrderController : BaseApiController
    {
        readonly IOrderBusiness orderBusiness;

        /// <summary>
        ///   Controller
        /// </summary>
        /// <param name="orderBusiness"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param> 
        public CustomerOrderController(
            IOrderBusiness orderBusiness, 
            IMapper mapper, 
            ILogger<CustomerOrderController> logger) : base(logger, mapper)
        {
            this.orderBusiness = orderBusiness;
        }

        /// <summary>
        /// Obtiene los datos de la ordenes
        /// </summary>
        /// <returns>lista de ordenes</returns>
        /// <response code="200">Devuelve la lista de ordenes</response>
        /// <response code="400">No ha pasado las validaciones</response>    
        [HttpGet("{userId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ComponentError), (int)HttpStatusCode.BadRequest)]
        public ActionResult<List<OrdersUserDtoVM>> Get(int userId)
        {
            var resp = orderBusiness.GetOrdenUserDto(userId);
            List<OrdersUserDtoVM> response = _mapper.Map<List<OrdersUserDto>, List<OrdersUserDtoVM>>(resp);
            return response;
        }
    }
}
