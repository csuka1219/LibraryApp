using System.Net.Http.Json;
using Library;

namespace LibrarianClient.Services
{
    public class LibraryMemberService: ILibraryMemberService
    {
        private readonly HttpClient _httpClient;

        public LibraryMemberService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<List<LibraryMember>?> GetAllLibraryMemberAsync() =>
            _httpClient.GetFromJsonAsync<List<LibraryMember>>("LibraryMember");
        public Task<List<LibraryMember>?> GetActiveLibraryMembersAsync() =>
            _httpClient.GetFromJsonAsync<List<LibraryMember>>("LibraryMember/active");

        public Task<LibraryMember?> GetLibraryMemberByIdAsync(int id) =>
            _httpClient.GetFromJsonAsync<LibraryMember?>($"LibraryMember/{id}");

        public async Task UpdateLibraryMemberAsync(int id, LibraryMember person) =>
            await _httpClient.PutAsJsonAsync($"LibraryMember/{id}", person);

        public async Task DeleteLibraryMemberAsync(int id) =>
            await _httpClient.DeleteAsync($"LibraryMember/{id}");

        public async Task AddLibraryMemberAsync(LibraryMember person) =>
            await _httpClient.PostAsJsonAsync("LibraryMember", person);
    }
}
