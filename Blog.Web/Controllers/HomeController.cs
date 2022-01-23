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
        private readonly string mAPIBaseUrl = "http://localhost:8085/api";
        public HomeController(ILogger<HomeController> logger)
        {
            mLogger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Post()
        {
            try
            {
                var tClient = new HttpClient();
                var tRequest = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(mAPIBaseUrl + "/post"),
                };

                using (var tResponse = await tClient.SendAsync(tRequest))
                {
                    if (tResponse.IsSuccessStatusCode)
                    {
                        var tPostResponse = await tResponse.Content.ReadAsStringAsync();
                        IEnumerable<Post> tPosts = JsonConvert.DeserializeObject<List<Post>>(tPostResponse);
                        var tCount = tPosts!.Count();
                        return Json(new { result = tPosts, count = tCount });
                    }
                }
            }
            catch (Exception e)
            {
                mLogger.LogError(e.Message);
            }

            return NotFound();
        }
        


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
