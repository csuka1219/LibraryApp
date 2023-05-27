using Library;

namespace LibraryWebApi.Repositories
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAllBooksAsync();

        Task<Book> GetBookByIdAsync(int id);

        Task<int> AddBookAsync(Book book);

        Task UpdateBookAsync(Book book);

        Task DeleteBookAsync(Book book);

    }
}
