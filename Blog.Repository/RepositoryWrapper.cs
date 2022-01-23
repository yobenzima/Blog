using System.Threading.Tasks;
using Blog.Contracts;
using Blog.Entities;
using Blog.Repository;


namespace Blog.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly RepositoryContext mRepositoryContext;
        private IContactRepository mContactRepository;
        private IPostRepository mPostRepository;

        public IContactRepository Contact {
            get 
            {
                if(mContactRepository == null)
                {
                    mContactRepository = new ContactRepository(mRepositoryContext);
                }

                return mContactRepository;
            }
        }

        public IPostRepository Post {
            get
            {
                if(mPostRepository == null)
                {
                    mPostRepository = new PostRepository(mRepositoryContext);
                }

                return mPostRepository;
            }
        }

        public RepositoryWrapper(RepositoryContext repositoryContext)
        {
            mRepositoryContext = repositoryContext;
        }

        public async Task SaveAsync()
        {
           await mRepositoryContext.SaveChangesAsync();
        }
    }
}