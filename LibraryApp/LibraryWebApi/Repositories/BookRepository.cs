using Library;
using LibraryWebApi.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebApi.Repositories
{
    public class BookRepository:IBookRepository
    {
        private readonly LibraryDbContext _context;

        public BookRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<List<Book>> GetAllBooksAsync()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            return await _context.Books.FindAsync(id);
        }

        public async Task<int> AddBookAsync(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return await _context.Books.MaxAsync(b => b.InvNumber);
        }

        public async Task UpdateBookAsync(Book book)
        {
            _context.Entry(book).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBookAsync(Book book)
        {
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Book>> GetAvailableBooksAsync()
        {
            return await _context.Books.Where(b => !_context.Loans.Select(l => l.InvNumber).Contains(b.InvNumber)).ToListAsync();
        }

        public async Task<List<Book>> GetLoanedBooksAsync()
        {
            return await _context.Books.Where(b => _context.Loans.Select(l => l.InvNumber).Contains(b.InvNumber)).ToListAsync();
        }
    }
}
