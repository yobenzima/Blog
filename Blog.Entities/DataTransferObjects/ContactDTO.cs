using System;

namespace Blog.Entities.DataTransferObjects
{
    public class ContactDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public DateTime CreateTS { get; set; } 
        public DateTime UpdateTS { get; set; }
    }
}