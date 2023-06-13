using System.Net.Http;
using System.Net.Http.Json;
using Library;

namespace ReaderClient.Services
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


    }
}
