using AutoMapper;
using System.Net;
using System;
using System.IO;
using Microsoft.Extensions.Logging;
using Project.TakuGames.Model.Business;
using Project.TakuGames.Model.Domain;
using Project.TakuGames.Model.ViewModels;
using Project.TakuGames.Model.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using Newtonsoft.Json;

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
         readonly IWebHostEnvironment hostingEnvironment;
        readonly IConfiguration config;
        readonly string coverImageFolderPath = string.Empty;
        
        /// <summary>
        ///  Controller
        /// </summary>
        /// <param name="userBusiness"></param>
        /// <param name="cartBusiness"></param>
        /// <param name="hostEnvironment"></param>
        /// <param name="config"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param> 
        public UserController(
            IUserBusiness userBusiness,
            ICartBusiness cartBusiness,
            IConfiguration config,
            IWebHostEnvironment hostEnvironment,
            IMapper mapper,
            ILogger<UserController> logger) : base(logger, mapper)
        {
             this.config = config;
            this.cartBusiness = cartBusiness;
            this.userBusiness = userBusiness;

            this.hostingEnvironment = hostEnvironment;
            this.coverImageFolderPath = Path.Combine(hostingEnvironment.WebRootPath, "UserImage");
            if (!Directory.Exists(coverImageFolderPath))
            {
                Directory.CreateDirectory(coverImageFolderPath);
            }
        }

               /// <summary>
        /// Obtiene el recuento del artículo en el carrito de compras
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>El recuento de artículos en el carrito de compras.</returns>

        [HttpGet("{Id:int}" )]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ComponentError), (int)HttpStatusCode.BadRequest)]
        public ActionResult<int> Get(int Id)
        {
            var response = cartBusiness.GetCartItemCount(Id);
            return response;
        }
        /// <summary>
        /// Se registra un nuevo usuario
        /// </summary>
        /// 
        [HttpPost,DisableRequestSizeLimit]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ComponentError), (int)HttpStatusCode.BadRequest)]
        public ActionResult<UserMasterVM> Post( )
        {
            UserMasterVM gamevm = JsonConvert.DeserializeObject<UserMasterVM>(Request.Form["UserFormData"].ToString());
            var gam = _mapper.Map<UserMasterVM, UserMaster>(gamevm);


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
                    gam.UserImage = fileName;
                }
            }
            else
            {
                gam.UserImage = config["DefaultCoverImageFile"];
            }


            // var userNew = _mapper.Map<UserMasterVM, UserMaster>(userMaster);
            var createdUser = userBusiness.RegisterUser(gam);
            UserMasterVM response = _mapper.Map<UserMaster, UserMasterVM>(createdUser); 
            return Created($"{Request.Path}/{response.UserId}",response);
        }
    }
}
