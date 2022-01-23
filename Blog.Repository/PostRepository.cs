using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Contracts;
using Blog.Entities;
using Blog.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog.Repository
{
    public class PostRepository : RepositoryBase<Post>, IPostRepository
    {
        public PostRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Post>> GetAllPosts()
        {
            return await FindAll()
                .OrderBy(p => p.CreateTS)
                .ToListAsync();
        }

        public async Task<Post> GetPostById(int id)
        {
            return await FindByCondition(post => post.Id.Equals(id))
                .FirstOrDefaultAsync();
        }

        public void CreatePost(Post post)
        {
            Create(post);
        }

        public void UpdatePost(Post post)
        {
            Update(post);
        }

        public void DeletePost(Post post)
        {
            Delete(post);
        }
    }
}
