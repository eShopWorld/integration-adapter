using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IntegrationAdapter.Controllers
{
    /// <summary>
    /// health probe controller
    /// </summary>
    ///
    [ApiVersionNeutral]
    [Route("[controller]")]
    [Produces("application/json")]
    [AllowAnonymous]
    public class ProbeController : Controller
    {
        /// <summary>
        /// empty get to serve as health probe endpoint
        /// </summary>
        [HttpGet]
        public IActionResult Get()
        {
            //todo should there be more dependencies in this check.
            return new OkResult();
        }
    }
}