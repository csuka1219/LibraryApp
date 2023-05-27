using Library;

namespace LibrarianClient.Services
{
    public interface IBookService
    {
        Task<List<Book>?> GetAllBookAsync();

        Task<Book?> GetBookByIdAsync(int id);

        Task UpdateBookAsync(int id, Book person);

        Task DeleteBookAsync(int id);

        Task<int> AddBookAsync(Book person);
    }
}
