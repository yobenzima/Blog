using System;
using System.ComponentModel.DataAnnotations;

namespace Blog.Entities.Models
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreateTS { get; set; } = DateTime.Now;
        public DateTime UpdateTS { get; set; }
    }
}