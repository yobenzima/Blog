using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.Entities.Models;

namespace Blog.Contracts
{
    public interface IContactRepository : IRepositoryBase<Contact>
    {
        Task<IEnumerable<Contact>> GetAllContacts();
        Task<Contact> GetContactById(int id);
        void CreateContact(Contact contact);
        void UpdateContact(Contact contact);
        void DeleteContact(Contact contact);
    }
}