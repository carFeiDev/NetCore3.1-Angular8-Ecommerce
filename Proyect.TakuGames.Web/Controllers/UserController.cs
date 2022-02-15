using AutoMapper;
using System;
using System.IO;
using System.Net;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Project.TakuGames.Model.Business;
using Project.TakuGames.Model.Domain;
using Project.TakuGames.Model.ViewModels;
using Project.TakuGames.Model.Helpers;

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
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly IConfiguration config;
        private readonly string coverImageFolderPath = string.Empty;
        
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
        /// Obtiene los datos del Usuario por el id 
        /// </summary>
        /// <returns>Datos del Usuario</returns>
        /// <param name="UserId"></param>
        /// <response code="200">Datos del Usuario</response>
        /// <response code="400">No ha pasado las validaciones</response>    
        /// <response code="404">No se encontró al usuario</response>    
        [HttpGet("GetUser/{UserId}",Name = "GetUser")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ComponentError), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public ActionResult<UserMasterVM> GetUser(int UserId)
        {
           var resp = userBusiness.GetUser(UserId);   
           if (resp == null )
            {
                return NotFound();
            }
            UserMasterVM response = _mapper.Map<UserMaster, UserMasterVM>(resp);
            return response;
        }
        /// <summary>
        /// Obtiene el recuento del artículo en el carrito de compras
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>El recuento de artículos en el carrito de compras.</returns>

        [HttpGet("{Id}",Name = "GetCartItemNumber")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ComponentError), (int)HttpStatusCode.BadRequest)]
        public ActionResult<int> GetCartItemNumber(int Id)
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
           
            UserMasterVM newUserVm = JsonConvert.DeserializeObject<UserMasterVM>(Request.Form["UserFormData"].ToString());
            UserMasterVM newUserWithImageVm = UploadImage(newUserVm);
            UserMaster newUser = _mapper.Map<UserMasterVM, UserMaster>(newUserWithImageVm);
            UserMaster createdUser = userBusiness.RegisterUser(newUser);
            UserMasterVM response = _mapper.Map<UserMaster, UserMasterVM>(createdUser); 
            return Created($"{Request.Path}/{response.UserId}",response);
        }
           /// <summary>
        /// Modifica un usuario en la app
        /// </summary>
        /// <returns>Juego modificado</returns>
        /// <response code="200">usuario modificado</response>
        /// <response code="400">No ha pasado las validaciones</response>  
        /// <response code="404">No se encontró  el usuario</response>  
        [HttpPut("{Id:int}")]
        //[Authorize(Policy = UserRoles.Admin)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ComponentError), (int)HttpStatusCode.BadRequest)]
        public ActionResult<UserMasterVM> Put()
        {
            UserMasterVM userEditedVm = JsonConvert.DeserializeObject<UserMasterVM>(Request.Form["UserFormData"].ToString());

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
                    userEditedVm.UserImage = fileName;
                }
            }
            UserMaster userEdited = _mapper.Map<UserMasterVM,UserMaster>(userEditedVm);
            UserMaster resp = userBusiness.EditUser(userEdited);
            UserMasterVM response = _mapper.Map<UserMaster, UserMasterVM>(resp);
            return response;
        }

        private UserMasterVM UploadImage(UserMasterVM newUserVm)
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
                    newUserVm.UserImage = fileName;
                }
            }
            else
            {
                newUserVm.UserImage = config["DefaultCoverImageFile"];
            }
            return newUserVm;
        }
    }
}
