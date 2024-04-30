using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Library_MS_Web_API.Models
{
    [Table("Members")]
    public class Members
    {
        [Key]
        public int MemberID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }  
        public string? PhoneNumber { get; set; } 
        public DateTime RegistrationDate { get; set; }

    }
}
