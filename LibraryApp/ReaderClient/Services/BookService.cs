using System.Net.Http.Json;
using Library;

namespace ReaderClient.Services
{
    public class BookService : IBookService
    {
        private readonly HttpClient httpClient;

        public BookService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public Task<List<Book>?> GetAllBookAsync() =>
            httpClient.GetFromJsonAsync<List<Book>>("Book");

        public Task<List<Book>?> GetAvailableBooksAsync() =>
            httpClient.GetFromJsonAsync<List<Book>>("Book/available");

        public Task<List<Book>?> GetLoanedBooksAsync() =>
            httpClient.GetFromJsonAsync<List<Book>>("Book/loaned");

        public Task<Book?> GetBookByIdAsync(int id) =>
            httpClient.GetFromJsonAsync<Book?>($"Book/{id}");


    }
}
