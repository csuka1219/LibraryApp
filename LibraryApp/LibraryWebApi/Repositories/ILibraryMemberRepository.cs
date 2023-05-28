using Library;

namespace LibraryWebApi.Repositories
{
    public interface ILibraryMemberRepository
    {
        Task<List<LibraryMember>> GetAllLibraryMembersAsync();

        Task<LibraryMember> GetLibraryMemberByIdAsync(int id);

        Task<int> AddLibraryMemberAsync(LibraryMember libraryMember);

        Task UpdateLibraryMemberAsync(LibraryMember libraryMember);

        Task DeleteLibraryMemberAsync(LibraryMember libraryMember);

		Task<List<LibraryMember>> GetActiveLibraryMembersAsync();
	}
}
