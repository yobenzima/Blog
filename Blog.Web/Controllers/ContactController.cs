using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AutoMapper;
using Blog.Web.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Blog.Web.Controllers
{
    [Route("contact")]
    public class ContactController : Controller
    {
        private readonly ILogger<ContactController> mLogger;
        private readonly string mAPIBaseUrl = "http://localhost:8085/api";

        public ContactController(ILogger<ContactController> logger)
        {
            mLogger = logger;
        }

        [HttpGet("/contacts")]
        public async Task<IActionResult> AllContacts()
        {
            try
            {
                var tClient = new HttpClient();
                var tRequest = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(mAPIBaseUrl + "/contact"),
                };

                using (var tResponse = await tClient.SendAsync(tRequest))
                {
                    if (tResponse.IsSuccessStatusCode)
                    {
                        var tPostResponse = await tResponse.Content.ReadAsStringAsync();
                        IEnumerable<Contact> tContacts = JsonConvert.DeserializeObject<List<Contact>>(tPostResponse);
                        return View(tContacts);
                    }
                }
            }
            catch (Exception e)
            {
                mLogger.LogError(e.Message);
            }

            return StatusCode(404);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View(new Contact());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Contact contact)
        {
            try
            {
                using (HttpClient tClient = new HttpClient())
                {
                    HttpResponseMessage tResponse = await tClient.PostAsJsonAsync(mAPIBaseUrl + "/contact", contact);

                    if (tResponse.IsSuccessStatusCode)
                    {
                        var tApiResponse = await tResponse.Content.ReadAsStringAsync();
                        Contact tContact = JsonConvert.DeserializeObject<Contact>(tApiResponse);
                        return View(tContact);
                    }
                }
            }
            catch (Exception e)
            {
                mLogger.LogError(e.Message);
            }
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Contact(int id)
        {
            try
            {
                var tClient = new HttpClient();
                var tRequest = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(mAPIBaseUrl + $"/contact/{id}"),
                };

                using (var tResponse = await tClient.SendAsync(tRequest))
                {
                    if (tResponse.IsSuccessStatusCode)
                    {
                        var tPostResponse = await tResponse.Content.ReadAsStringAsync();
                        Contact tContact = JsonConvert.DeserializeObject<Contact>(tPostResponse);
                        return View(tContact);
                    }
                }
            }
            catch (Exception e)
            {
                mLogger.LogError(e.Message);
            }

            return StatusCode(404);
        }
    }
}