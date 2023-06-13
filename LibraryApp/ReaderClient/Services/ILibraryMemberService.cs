using Library;

namespace ReaderClient.Services
{
    public interface ILibraryMemberService
    {
        Task<List<LibraryMember>?> GetAllLibraryMemberAsync();

        Task<List<LibraryMember>?> GetActiveLibraryMembersAsync();

        Task<LibraryMember?> GetLibraryMemberByIdAsync(int id);


    }
}
