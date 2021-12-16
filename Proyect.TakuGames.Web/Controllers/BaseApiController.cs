using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Proyect.TakuGames.Web.Controllers
{
  /// <summary>
    /// Ctrl Base
    /// </summary>
    public class BaseApiController : ControllerBase
    {
        /// <summary>
        /// Logger
        /// </summary>
        protected readonly ILogger _logger;
        /// <summary>
        /// mapper
        /// </summary>
        protected readonly IMapper _mapper;
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        public BaseApiController(ILogger logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }
    }
}
