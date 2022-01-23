using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.Entities.Models;

namespace Blog.Contracts
{
    public interface IPostRepository : IRepositoryBase<Post>
    {
        Task<IEnumerable<Post>> GetAllPosts();
        Task<Post> GetPostById(int id);
        void CreatePost(Post post);
        void UpdatePost(Post post);
        void DeletePost(Post post);
    }
}