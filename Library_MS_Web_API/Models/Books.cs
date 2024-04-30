using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library_MS_Web_API.Models
{
    [Table("Books")]
    public class Books
    {
        [Key]
        public int BookID { get; set; }
        public string? Title { get; set; }
        public string? ISBN { get; set; }
        public DateTime PublishedDate { get; set; }
        public int AvailableCopies { get; set; } 
        public int AuthorID { get; set; }
        public int TotalCopies { get; set; }
        [ForeignKey("AuthorID")]
        public Authors? authors { get; set; }
    }
}
