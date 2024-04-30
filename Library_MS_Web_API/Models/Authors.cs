using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library_MS_Web_API.Models
{
    [Table("Authors")]
    public class Authors
    {
        [Key]
        public int AuthorID { get; set; }
        public string? AuthorName { get; set; }
        public string? AuthorBio { get; set; } 
        public string? AuthorPhoneNo { get; set; }
    }
}
