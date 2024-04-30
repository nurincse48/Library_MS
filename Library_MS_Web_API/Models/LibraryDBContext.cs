using Library_MS_Web_API.Models.Authentication;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Library_MS_Web_API.Models
{
    public class LibraryDBContext:IdentityDbContext<ApplicationUser>
    {
        public LibraryDBContext(DbContextOptions<LibraryDBContext> options):base(options)
        { 
        
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public DbSet<Authors> authors { get; set; }
        public DbSet<Members> members { get; set; }
        public DbSet<Books> books { get; set; }
        public DbSet<BorrowdBooks> borrowdBooks { get; set; }
    }
}
