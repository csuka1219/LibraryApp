using System.Net.Http.Json;
using Library;

namespace LibrarianClient.Services
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

        public async Task UpdateBookAsync(int id, Book person) =>
            await httpClient.PutAsJsonAsync($"Book/{id}", person);

        public async Task DeleteBookAsync(int id) =>
            await httpClient.DeleteAsync($"Book/{id}");

        public async Task<int> AddBookAsync(Book person)
        {
            HttpResponseMessage response = await httpClient.PostAsJsonAsync("Book", person);
            return await response.Content.ReadFromJsonAsync<int>();
        }
    }
}
