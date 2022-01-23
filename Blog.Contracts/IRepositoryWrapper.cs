using System.Threading.Tasks;

namespace Blog.Contracts
{
    public interface IRepositoryWrapper
    {
        IPostRepository Post { get; }
        IContactRepository Contact { get; }
        Task SaveAsync();
    }
}