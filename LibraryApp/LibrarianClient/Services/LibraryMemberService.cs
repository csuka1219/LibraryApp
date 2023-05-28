using System.Net.Http;
using System.Net.Http.Json;
using Library;

namespace LibrarianClient.Services
{
    public class LibraryMemberService : ILibraryMemberService
    {
        private readonly HttpClient httpClient;

        public LibraryMemberService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public Task<List<LibraryMember>?> GetAllLibraryMemberAsync() =>
            httpClient.GetFromJsonAsync<List<LibraryMember>>("LibraryMember");

        public Task<List<LibraryMember>?> GetActiveLibraryMembersAsync() =>
            httpClient.GetFromJsonAsync<List<LibraryMember>>("LibraryMember/active");

        public Task<LibraryMember?> GetLibraryMemberByIdAsync(int id) =>
            httpClient.GetFromJsonAsync<LibraryMember?>($"LibraryMember/{id}");

        public async Task UpdateLibraryMemberAsync(int id, LibraryMember person) =>
            await httpClient.PutAsJsonAsync($"LibraryMember/{id}", person);

        public async Task DeleteLibraryMemberAsync(int id) =>
            await httpClient.DeleteAsync($"LibraryMember/{id}");

        public async Task<int> AddLibraryMemberAsync(LibraryMember person)
        {
            HttpResponseMessage response = await httpClient.PostAsJsonAsync("LibraryMember", person);
            return await response.Content.ReadFromJsonAsync<int>();
        }
    }
}
