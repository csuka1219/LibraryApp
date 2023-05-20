using Library;

namespace LibraryWebApi.Repositories
{
    public interface ILibraryMemberRepository
    {
        Task<List<LibraryMember>> GetAllLibraryMembersAsync();

        Task<LibraryMember> GetLibraryMemberByIdAsync(int id);

        Task AddLibraryMemberAsync(LibraryMember libraryMember);

        Task UpdateLibraryMemberAsync(LibraryMember libraryMember);

        Task DeleteLibraryMemberAsync(LibraryMember libraryMember);
    }
}
