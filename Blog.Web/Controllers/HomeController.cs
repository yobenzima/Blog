using Blog.Web.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Blog.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> mLogger;
        public HomeController(ILogger<HomeController> logger)
        {
            mLogger = logger;
        }

        [HttpGet]
        public async Task<IActionResult>  Index()
        {
            return View(); }
    }
}