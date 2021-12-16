using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Project.TakuGames.Model.Business;
using Project.TakuGames.Model.Domain;
using AutoMapper;

namespace Proyect.TakuGames.Web.Controllers
{
     /// <summary>
    /// Datos de userController
    /// </summary>   
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class LoginController : BaseApiController
    {
        readonly IUserBusiness _userBusiness;

        /// <summary>
        ///   Controller
        /// </summary>
        /// <param name="userBusiness"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
      
        public LoginController(
            IUserBusiness userBusiness,
            IMapper mapper,
            ILogger<LoginController> logger) : base(logger, mapper)
        {
            this._userBusiness = userBusiness;
        }
        /// <summary>
        /// Login para la app
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        
        [HttpPost]
        public IActionResult Login([FromBody] UserMaster login)
        {
            IActionResult response = Unauthorized();
            UserMaster user = _userBusiness.AuthenticateUser(login);
            if (user != null)
            {
                var tokenString = _userBusiness.GenerateJSONWebToken(user);
                response = Ok(new
                {
                    token = tokenString,
                    userDetails = user,
                });
            }
            return response;
        }
    }
}
