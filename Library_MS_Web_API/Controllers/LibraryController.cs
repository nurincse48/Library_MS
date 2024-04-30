using Library_MS_Web_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library_MS_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryController : ControllerBase
    {
        public readonly LibraryDBContext _libraryDBContext;
        public LibraryController(LibraryDBContext libraryDBContext) {
            _libraryDBContext = libraryDBContext;
        }
        //----------------------------- API for Author ---------------------------------
        [HttpGet]
        [Route("GetAllAuthors")]
        public async Task<IActionResult> GetAllAuthors()
        {
            return Ok(await _libraryDBContext.authors.ToListAsync());
        }
        [HttpGet]
        [Route("GetAllAuthorsById")]
        public async Task<IActionResult> GetAllAuthorsById(int authorId)
        {
            return Ok(await _libraryDBContext.authors.FindAsync(authorId));
        }
        [HttpPost]
        [Route("CreateAuthor")]
        public async Task<IActionResult> CreateAuthor(Authors authors)
        {
            var ExistAuthor =  _libraryDBContext.authors.Where(x => x.AuthorPhoneNo == authors.AuthorPhoneNo).FirstOrDefault();
            if (ExistAuthor == null)
            {
                Authors newAuthors = new Authors()
                {
                    AuthorName = authors.AuthorName,
                    AuthorBio = authors.AuthorBio,
                    AuthorPhoneNo = authors.AuthorPhoneNo,
                };
                _libraryDBContext.authors.Add(newAuthors);
                var result = await _libraryDBContext.SaveChangesAsync();
                if (result > 0)
                {
                    return Ok("Author is Created Successfully.");
                }
                else
                {
                    return BadRequest("Author Creation is Failed.");
                }
            }
            else
            { 
                return Ok("Author already exist.");
            }
        }
        [HttpPost]
        [Route("UpdateAuthor")]
        public async Task<IActionResult> UpdateAuthor(Authors authors)
        {
            var ExistAuthor = await _libraryDBContext.authors.FindAsync(authors.AuthorID);
            if (ExistAuthor != null)
            {
                _libraryDBContext.Entry(ExistAuthor).State = EntityState.Detached;
                ExistAuthor.AuthorName = authors.AuthorName;
                ExistAuthor.AuthorBio = authors.AuthorBio;
                ExistAuthor.AuthorPhoneNo = authors.AuthorPhoneNo;
                _libraryDBContext.Entry(ExistAuthor).State |= EntityState.Modified;
            }
            var result = await _libraryDBContext.SaveChangesAsync();
            if (result > 0)
            {
                 return Ok("Author is Updated Successfully.");
            }
            else
            {
               return BadRequest("Author Update is Failed.");
            }
            
          
        }

        [HttpDelete]
        [Route("DeleteAuthorById")]
        public async Task<IActionResult> DeleteAuthorById(int authorId)
        {
           var ExistAuthor = await _libraryDBContext.authors.FindAsync(authorId);
            if (ExistAuthor == null)
            {
                return NotFound();
            }

            _libraryDBContext.authors.Remove(ExistAuthor);
            await _libraryDBContext.SaveChangesAsync();

            return Ok("Author Delete Successfully.");
        }

        //----------------------------- API for Member ---------------------------------

        [HttpGet]
        [Route("GetAllMembers")]
        public async Task<IActionResult> GetAllMembers()
        {
            return Ok(await _libraryDBContext.members.ToListAsync());
        }
        [HttpGet]
        [Route("GetAllMembersById")]
        public async Task<IActionResult> GetAllMembersById(int memberId)
        {
            return Ok(await _libraryDBContext.members.FindAsync(memberId));
        }
        [HttpPost]
        [Route("CreateMember")]
        public async Task<IActionResult> CreateMember(Members members)

        {
            var ExistMember = _libraryDBContext.members.Where(x => x.PhoneNumber == members.PhoneNumber).FirstOrDefault();
            if (ExistMember == null)
            {
                Members newMember = new Members()
                {
                   FirstName = members.FirstName,
                   LastName = members.LastName,
                   Email = members.Email,
                   PhoneNumber = members.PhoneNumber,
                   RegistrationDate = members.RegistrationDate,
                };
                _libraryDBContext.members.Add(newMember);
                var result = await _libraryDBContext.SaveChangesAsync();
                if (result > 0)
                {
                    return Ok("Member is Created Successfully.");
                }
                else
                {
                    return BadRequest("Member Creation is Failed.");
                }
            }
            else
            {
                return Ok("Member already exist.");
            }
        }
        [HttpPost]
        [Route("UpdateMember")]
        public async Task<IActionResult> UpdateMember(Members members)

        {
            var ExistMember = await _libraryDBContext.members.FindAsync(members.MemberID);
            if (ExistMember != null)
            {
                _libraryDBContext.Entry(ExistMember).State = EntityState.Detached;
                ExistMember.FirstName = members.FirstName;
                ExistMember.LastName = members.LastName;
                ExistMember.Email = members.Email;
                ExistMember.PhoneNumber = members.PhoneNumber;
                ExistMember.RegistrationDate = members.RegistrationDate;
                _libraryDBContext.Entry(ExistMember).State |= EntityState.Modified;
            }
            var result = await _libraryDBContext.SaveChangesAsync();
            if (result > 0)
            {
                return Ok("Member is Updated Successfully.");
            }
            else
            {
                return BadRequest("Member Update is Failed.");
            }


        }


        [HttpDelete]
        [Route("DeleteMemberById")]
        public async Task<IActionResult> DeleteMemberById(int memberId)
        {
            var ExistMember = await _libraryDBContext.members.FindAsync(memberId);
            if (ExistMember == null)
            {
                return NotFound();
            }

            _libraryDBContext.members.Remove(ExistMember);
            await _libraryDBContext.SaveChangesAsync();

            return Ok("Member Delete Successfully.");
        }

        //----------------------------- API for BOOK ---------------------------------

        [HttpGet]
        [Route("GetAllBooks")]
        public async Task<IActionResult> GetAllBooks()
        {
            //return Ok(await _libraryDBContext.books.ToListAsync());
            var result = await _libraryDBContext.books.Include(e => e.authors).ToListAsync();
            return Ok(result);
        }
        [HttpGet]
        [Route("GetAllBooksById")]
        public async Task<IActionResult> GetAllBooksById(int bookId)
        {
            return Ok(await _libraryDBContext.books.FindAsync(bookId));
        }
        [HttpPost]
        [Route("CreateBook")]
        public async Task<IActionResult> CreateBook(Books books)

        {
            var ExistBook = _libraryDBContext.books.Where(x => x.Title == books.Title).FirstOrDefault();
            if (ExistBook == null)
            {
                Books newBook = new Books()
                {
                    Title = books.Title,
                    ISBN = books.ISBN,
                    PublishedDate = books.PublishedDate,
                    AvailableCopies = books.AvailableCopies,
                    TotalCopies = books.TotalCopies,
                    AuthorID = books.AuthorID,
                };
                _libraryDBContext.books.Add(newBook);
                var result = await _libraryDBContext.SaveChangesAsync();
                if (result > 0)
                {
                    return Ok("Book is Created Successfully.");
                }
                else
                {
                    return BadRequest("Book Creation is Failed.");
                }
            }
            else
            {
                return Ok("Book already exist.");
            }
        }
        [HttpPost]
        [Route("UpdateBook")]
        public async Task<IActionResult> UpdateBook(Books books)

        {
            var ExistBook = await _libraryDBContext.books.FindAsync(books.BookID);
            if (ExistBook != null)
            {
                _libraryDBContext.Entry(ExistBook).State = EntityState.Detached;
                ExistBook.Title = books.Title;
                ExistBook.ISBN = books.ISBN;
                ExistBook.PublishedDate = books.PublishedDate;
                ExistBook.AvailableCopies = books.AvailableCopies;
                ExistBook.TotalCopies = books.TotalCopies;
                ExistBook.AuthorID = books.AuthorID;
                _libraryDBContext.Entry(ExistBook).State |= EntityState.Modified;
            }
            var result = await _libraryDBContext.SaveChangesAsync();
            if (result > 0)
            {
                return Ok("Book is Updated Successfully.");
            }
            else
            {
                return BadRequest("Book Update is Failed.");
            }


        }

        [HttpDelete]
        [Route("DeleteBookById")]
        public async Task<IActionResult> DeleteBookById(int bookId)
        {
            var ExistBook = await _libraryDBContext.books.FindAsync(bookId);
            if (ExistBook == null)
            {
                return NotFound();
            }

            _libraryDBContext.books.Remove(ExistBook);
            await _libraryDBContext.SaveChangesAsync();

            return Ok("Book Delete Successfully.");
        }

        //----------------------------- API for Borrowd Book ---------------------------------

        [HttpGet]
        [Route("GetAllBorrowdBook")]
        public async Task<IActionResult> GetAllBorrowdBook()
        {
            //return Ok(await _libraryDBContext.books.ToListAsync());
            var result = await _libraryDBContext.borrowdBooks.Include(e => e.members).Include(e =>e.books).ToListAsync();
            return Ok(result);
        }
        [HttpGet]
        [Route("GetAllBorrowdBookById")]
        public async Task<IActionResult> GetAllBorrowdBookById(int borrowdBookId)
        {
            return Ok(await _libraryDBContext.borrowdBooks.FindAsync(borrowdBookId));
        }
        [HttpPost]
        [Route("CreateBorrowdBook")]
        public async Task<IActionResult> CreateBorrowdBook(BorrowdBooks borrowdBooks)

        {
            var ExistBorrowdBook = _libraryDBContext.borrowdBooks.Where(x => x.BorrowedNo == borrowdBooks.BorrowedNo).FirstOrDefault();
            if (ExistBorrowdBook == null)
            {
                BorrowdBooks newBorrowedBook = new BorrowdBooks() 
                { 
                    BorrowedNo = borrowdBooks.BorrowedNo,
                    BorrowDate = borrowdBooks.BorrowDate,
                    ReturnDate = borrowdBooks.ReturnDate,
                    Status = borrowdBooks.Status,
                    MemberID = borrowdBooks.MemberID,
                    BookID = borrowdBooks.BookID,

                };
               
                _libraryDBContext.borrowdBooks.Add(newBorrowedBook);
                var result = await _libraryDBContext.SaveChangesAsync();
                if (result > 0)
                {
                    return Ok("Borrowd Book is Created Successfully.");
                }
                else
                {
                    return BadRequest("Borrowd Book Creation is Failed.");
                }
            }
            else
            {
                return Ok("Borrowd Book already exist.");
            }
        }
        [HttpPost]
        [Route("UpdateBorrowdBook")]
        public async Task<IActionResult> UpdateBorrowdBook(BorrowdBooks borrowdBook)

        {
            var ExistBorrowedBook = await _libraryDBContext.borrowdBooks.FindAsync(borrowdBook.BorrowID);
            if (ExistBorrowedBook != null)
            {
                _libraryDBContext.Entry(ExistBorrowedBook).State = EntityState.Detached;
                ExistBorrowedBook.BorrowedNo = borrowdBook.BorrowedNo;
                ExistBorrowedBook.BorrowDate = borrowdBook.BorrowDate;
                ExistBorrowedBook.ReturnDate = borrowdBook.ReturnDate;
                ExistBorrowedBook.Status = borrowdBook.Status;
                ExistBorrowedBook.MemberID = borrowdBook.MemberID;
                ExistBorrowedBook.BookID = borrowdBook.BookID;
                _libraryDBContext.Entry(ExistBorrowedBook).State |= EntityState.Modified;
            }
            var result = await _libraryDBContext.SaveChangesAsync();
            if (result > 0)
            {
                return Ok("BorrowdBook is Updated Successfully.");
            }
            else
            {
                return BadRequest("BorrowdBook Update is Failed.");
            }


        }

        [HttpDelete]
        [Route("DeleteBorrowdBookById")]
        public async Task<IActionResult> DeleteBorrowdBookById(int borrowedBookId)
        {
            var ExistBorrowedBook = await _libraryDBContext.borrowdBooks.FindAsync(borrowedBookId);
            if (ExistBorrowedBook == null)
            {
                return NotFound();
            }

            _libraryDBContext.borrowdBooks.Remove(ExistBorrowedBook);
            await _libraryDBContext.SaveChangesAsync();

            return Ok("Borrowd Book Delete Successfully.");
        }

    }
}
