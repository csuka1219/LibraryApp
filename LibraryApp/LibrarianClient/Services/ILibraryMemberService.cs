using Library;

namespace LibrarianClient.Services
{
    public interface ILibraryMemberService
    {
        Task<List<LibraryMember>?> GetAllLibraryMemberAsync();

        Task<List<LibraryMember>?> GetActiveLibraryMembersAsync();

        Task<LibraryMember?> GetLibraryMemberByIdAsync(int id);

        Task UpdateLibraryMemberAsync(int id, LibraryMember person);

        Task DeleteLibraryMemberAsync(int id);

        Task<int> AddLibraryMemberAsync(LibraryMember person);
    }
}
