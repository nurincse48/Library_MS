using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Library_MS_Web_API.Models
{
    [Table("BorrowdBooks")]
    public class BorrowdBooks
    {
        [Key]
        public int BorrowID { get; set; }  
        public int MemberID { get; set; }  
        public int BookID { get; set; }
        public string? BorrowedNo { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public string? Status { get; set; }

        [ForeignKey("MemberID")]
        public Members? members { get; set; } 
        [ForeignKey("BookID")]
        public Books? books { get; set; }
    }
}
