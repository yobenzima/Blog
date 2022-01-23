using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog.Entities
{
    public class RepositoryContext: DbContext
    {
        public RepositoryContext(DbContextOptions options)
            :base(options)
        {
        }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Contact> Contacts { get; set; }
    }
}
