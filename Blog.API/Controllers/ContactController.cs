using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Blog.Contracts;
using Blog.Entities.DataTransferObjects;
using Blog.Entities.Models;
using Microsoft.Extensions.Logging;

namespace Blog.API.Controllers
{
    [ApiController]
    [Route("api/contact")]
    public class ContactController : ControllerBase
    {
        private readonly ILogger<ContactController> mLogger;
        private readonly IRepositoryWrapper mRepositoryWrapper;
        private readonly IMapper mMapper;
        
        public ContactController(ILogger<ContactController> logger, IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            mLogger = logger;
            mRepositoryWrapper = repositoryWrapper;
            mMapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllContacts()
        {
            try
            {
                var tContacts = await mRepositoryWrapper.Contact.GetAllContacts();
                var tResult = mMapper.Map<IEnumerable<ContactDTO>>(tContacts);
                return Ok(tResult);

            }
            catch (Exception e)
            {
                mLogger.LogError($"Error occurred in GetAllContacts action: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("{id}", Name = "ContactById")]
        public async Task<IActionResult> GetContactById(int id)
        {
            try
            {
                var tContact = await mRepositoryWrapper.Contact.GetContactById(id);
                if (tContact is null)
                {
                    return NotFound();
                }
                else
                {
                    var tResult = mMapper.Map<ContactDTO>(tContact);
                    return Ok(tResult);
                }
            }
            catch (Exception e)
            {
                mLogger.LogError($"Error occurred in GetContactById action: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateContact([FromBody] ContactCreateDTO contact)
        {
            try
            {
                if (contact is null)
                {
                    return BadRequest("Contact object is null!");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object!");
                }

                var tContact = mMapper.Map<Contact>(contact);
                mRepositoryWrapper.Contact.CreateContact(tContact);
                await mRepositoryWrapper.SaveAsync();

                var tResult = mMapper.Map<ContactDTO>(tContact);

                return CreatedAtRoute("ContactById", new { id = tResult.Id }, tResult);
            }
            catch (Exception e)
            {
                mLogger.LogError($"Error occurred in CreateContact action: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContact(int id, [FromBody] ContactUpdateDTO contact)
        {
            try
            {
                if (contact is null)
                {
                    return BadRequest("Contact object is null!");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object!");
                }

                var tContact = await mRepositoryWrapper.Contact.GetContactById(id);
                if (tContact is null)
                {
                    return NotFound();
                }

                mMapper.Map(contact, tContact);
                tContact.UpdateTS = DateTime.Now; // Update the time contact was amended

                mRepositoryWrapper.Contact.UpdateContact(tContact);
                await mRepositoryWrapper.SaveAsync();

                return NoContent();
            }
            catch (Exception e)
            {
                mLogger.LogError($"Error occurred in UpdateContact action: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            try
            {

                var tContact = await mRepositoryWrapper.Contact.GetContactById(id);
                if (tContact is null)
                {
                    return NotFound();
                }

                mRepositoryWrapper.Contact.DeleteContact(tContact);
                await mRepositoryWrapper.SaveAsync();

                return NoContent();
            }
            catch (Exception e)
            {
                mLogger.LogError($"Error occurred in DeleteContact action: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
