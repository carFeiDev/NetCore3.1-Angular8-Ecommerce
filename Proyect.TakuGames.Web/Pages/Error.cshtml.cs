
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Proyect.TakuGames.Web.Pages
{
    /// <summary>
    /// Error model
    /// </summary> 
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class ErrorModel : PageModel
    {
        private readonly ILogger<ErrorModel> _logger;
      /// <summary>
      /// Controller
      /// </summary> 
        public ErrorModel(ILogger<ErrorModel> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// request
        /// </summary> 
        public string RequestId { get; set; }

        /// <summary>
        /// showRequestId
        /// </summary> 
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        /// <summary>
        /// onGet
        /// </summary> 
        public void OnGet()
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        }
    }
}
