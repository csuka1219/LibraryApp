using System.Net.Http.Json;
using Library;

namespace LibrarianClient.Services
{
    public class BookService: IBookService
    {
        private readonly HttpClient _httpClient;

        public BookService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<List<Book>?> GetAllBookAsync() =>
            _httpClient.GetFromJsonAsync<List<Book>>("Book");

        public Task<Book?> GetBookByIdAsync(int id) =>
            _httpClient.GetFromJsonAsync<Book?>($"Book/{id}");

        public async Task UpdateBookAsync(int id, Book person) =>
            await _httpClient.PutAsJsonAsync($"Book/{id}", person);

        public async Task DeleteBookAsync(int id) =>
            await _httpClient.DeleteAsync($"Book/{id}");

        public async Task AddBookAsync(Book person) =>
            await _httpClient.PostAsJsonAsync("Book", person);
    }
}
