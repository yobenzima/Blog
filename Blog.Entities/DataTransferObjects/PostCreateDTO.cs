using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entities.DataTransferObjects
{
    public class PostCreateDTO
    {
        [Required(ErrorMessage = "Post Title Cannot Be Empty!")]
        [StringLength(50, ErrorMessage = "Post Title Cannot Exceed 50 Characters!")]
        public string Title { get; set; }
        
        [Required(ErrorMessage = "Post Body (ContentBody) Cannot Be Empty!")]
        [StringLength(8000, ErrorMessage = "Post Body (ContentBody) Cannot Exceed 8000 Characters!")]
        public string ContentBody { get; set; }
    }
}
