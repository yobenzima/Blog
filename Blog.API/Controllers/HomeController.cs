using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.API.Controllers
{
    [ApiController]
    [Route("api/home")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> mLogger;

        public HomeController(ILogger<HomeController> logger)
        {
            mLogger = logger;
        }

        public IActionResult Index()
        {
            return Ok(new { Name = "Test"} );
        }
    }
}
