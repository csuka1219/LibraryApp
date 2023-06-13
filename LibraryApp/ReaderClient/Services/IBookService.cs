using Library;

namespace ReaderClient.Services
{
    public interface IBookService
    {
        Task<List<Book>?> GetAllBookAsync();

        Task<List<Book>?> GetAvailableBooksAsync();

        Task<List<Book>?> GetLoanedBooksAsync();

        Task<Book?> GetBookByIdAsync(int id);

    }
}
