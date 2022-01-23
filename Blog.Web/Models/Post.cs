using System;
using System.ComponentModel.DataAnnotations;

namespace Blog.Web.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreateTS { get; set; } = DateTime.Now;
        public DateTime UpdateTS { get; set; }
        
        [Required(ErrorMessage = "Post Title Cannot Be Empty!")]
        [StringLength(50, ErrorMessage = "Post Title Cannot Exceed 50 Characters!")]
        public string Title { get; set; }
        
        [Required(ErrorMessage = "Post Body (ContentBody) Cannot Be Empty!")]
        [StringLength(8000, ErrorMessage = "Post Body (ContentBody) Cannot Exceed 8000 Characters!")]
        public string ContentBody { get; set; }
      
    }
}