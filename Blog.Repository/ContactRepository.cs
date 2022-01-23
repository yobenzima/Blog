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
    public class ContactRepository : RepositoryBase<Contact>, IContactRepository
    {
        public ContactRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Contact>> GetAllContacts()
        {
            return await FindAll()
                .OrderBy(c => c.CreateTS)
                .ToListAsync();
        }

        public async Task<Contact> GetContactById(int id)
        {
            return await FindByCondition(c => c.Id.Equals(id))
                .FirstOrDefaultAsync();
        }

        public void CreateContact(Contact contact)
        {
            Create(contact);
        }

        public void UpdateContact(Contact contact)
        {
            Update(contact);
        }

        public void DeleteContact(Contact contact)
        {
            Delete(contact);
        }
    }
}
