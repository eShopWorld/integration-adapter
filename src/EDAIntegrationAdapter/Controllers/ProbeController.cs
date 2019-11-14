﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EDAIntegrationAdapter.Controllers
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
        public void Get()
        {
            
        }
    }
}