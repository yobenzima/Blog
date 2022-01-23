using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Entities.Models
{
    [Table("Contact")]
    public class Contact : BaseEntity
    {
        [Required(ErrorMessage = "Name Cannot Be Empty!")]
        [StringLength(30, ErrorMessage = "First Name Cannot Exceed 30 Characters!")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name Cannot Be Empty!")]
        [StringLength(50, ErrorMessage = "Last Name Cannot Exceed 50 Characters!")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Phone Number Cannot Be Empty!")]
        [StringLength(10, ErrorMessage = "Phone Number Cannot Exceed 10 Characters!")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email Address Cannot Be Empty!")]
        [StringLength(120, ErrorMessage = "Email Address Cannot Exceed 120 Characters!")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Message Cannot Be Empty!")]
        [StringLength(500, ErrorMessage = "Message Cannot Exceed 500 Characters!")]
        public string Message { get; set; }
    }
}
