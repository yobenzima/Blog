using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Blog.Web.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Blog.Web.Controllers
{
    [Route("post")]
    public class PostController : Controller
    {
        private readonly ILogger<PostController> mLogger;
        private readonly string mAPIBaseUrl = "http://localhost:8085/api";

        public PostController(ILogger<PostController> logger)
        {
            mLogger = logger;
        }

        [HttpGet("/posts")]
        public async Task<IActionResult> AllPosts()
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
                        return View(tPosts);
                    }
                }
            }
            catch (Exception e)
            {
                mLogger.LogError(e.Message);
            }

            return StatusCode(404);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Post(int id)
        {
            try
            {
                var tClient = new HttpClient();
                var tRequest = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(mAPIBaseUrl + $"/post/{id}"),
                };

                using (var tResponse = await tClient.SendAsync(tRequest))
                {
                    if (tResponse.IsSuccessStatusCode)
                    {
                        var tPostResponse = await tResponse.Content.ReadAsStringAsync();
                        Post tPost = JsonConvert.DeserializeObject<Post>(tPostResponse);
                        return View(tPost);
                    }
                    else
                    {
                        return StatusCode(404);
                    }
                }
            }
            catch (Exception e)
            {
                mLogger.LogError(e.Message);
            }

            return NotFound();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Post());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Post post)
        {
            try
            {
                using (HttpClient tClient = new HttpClient())
                {
                    HttpResponseMessage tResponse = await tClient.PostAsJsonAsync(mAPIBaseUrl + "/post", post);

                    if (tResponse.IsSuccessStatusCode)
                    {
                        var tApiResponse = await tResponse.Content.ReadAsStringAsync();
                        Post tPost = JsonConvert.DeserializeObject<Post>(tApiResponse);
                        return View(tPost);
                    }
                }
            }
            catch (Exception e)
            {
                mLogger.LogError(e.Message);
            }

            return NoContent();
        }
    }
}
