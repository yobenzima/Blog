using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Blog.Contracts;
using Blog.Entities.DataTransferObjects;
using Blog.Entities.Models;

namespace Blog.API.Controllers
{
    [ApiController]
    [Route("api/post")]
    public class PostController : ControllerBase
    {
        private readonly ILogger<PostController> mLogger;
        private readonly IRepositoryWrapper mRepositoryWrapper;
        private readonly IMapper mMapper;
        
        public PostController(ILogger<PostController> logger, IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            mLogger = logger;
            mRepositoryWrapper = repositoryWrapper;
            mMapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPosts()
        {
            try
            {
                var tPosts = await mRepositoryWrapper.Post.GetAllPosts();

                var tResult = mMapper.Map<IEnumerable<PostDTO>>(tPosts);
                return Ok(tResult);
            }
            catch (Exception e)
            {
                mLogger.LogError($"Error occurred in GetAllPosts action: {e.Message}");
                return StatusCode(500, $"Internal Server Error");
            }
        }

        [HttpGet("{id}", Name = "PostById")]
        public async Task<IActionResult> GetPostById(int id)
        {
            try
            {
                var tPost = await mRepositoryWrapper.Post.GetPostById(id);
                if (tPost is null)
                {
                    return NotFound();
                }
                else
                {
                    var tResult = mMapper.Map<PostDTO>(tPost);
                    return Ok(tResult);
                }
            }
            catch (Exception e)
            {
                mLogger.LogError($"Error occurred in GetPostById action: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] Post post)
        {
            try
            {
                if (post is null)
                {
                    return BadRequest("Post object is null!");
                }
                if(!ModelState.IsValid)
                {
                    return BadRequest("Invalid post object!");
                }

                var tPost = mMapper.Map<Post>(post);
                mRepositoryWrapper.Post.CreatePost(tPost);
                await mRepositoryWrapper.SaveAsync();

                var tResult = mMapper.Map<PostDTO>(tPost);

                return CreatedAtRoute("PostById", new {id = tResult.Id}, tResult);
            }
            catch (Exception e)
            {
                mLogger.LogError($"Error occurred in CreatePost action: {e.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
