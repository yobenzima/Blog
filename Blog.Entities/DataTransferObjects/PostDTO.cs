using System;

namespace Blog.Entities.DataTransferObjects
{
    public class PostDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ContentBody { get; set; }
        public DateTime CreateTS { get; set; } 
        public DateTime UpdateTS { get; set; }
    }
}