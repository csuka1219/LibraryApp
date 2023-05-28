using Library;
using LibraryWebApi.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebApi.Repositories
{
    public class LibraryMemberRepository:ILibraryMemberRepository
    {
        private readonly LibraryDbContext _context;

        public LibraryMemberRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<List<LibraryMember>> GetAllLibraryMembersAsync()
        {
            return await _context.LibraryMembers.ToListAsync();
        }

        public async Task<LibraryMember> GetLibraryMemberByIdAsync(int id)
        {
            return await _context.LibraryMembers.FindAsync(id);
        }

        public async Task<int> AddLibraryMemberAsync(LibraryMember libraryMember)
        {
            _context.LibraryMembers.Add(libraryMember);
            await _context.SaveChangesAsync();
            return await _context.LibraryMembers.MaxAsync(b => b.ReaderNumber);
        }

        public async Task UpdateLibraryMemberAsync(LibraryMember libraryMember)
        {
            _context.Entry(libraryMember).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteLibraryMemberAsync(LibraryMember libraryMember)
        {
            _context.LibraryMembers.Remove(libraryMember);
            await _context.SaveChangesAsync();
        }

        public async Task<List<LibraryMember>> GetActiveLibraryMembersAsync()
        {
            return await _context.LibraryMembers.Where(lm => _context.Loans.Select(l => l.ReaderNumber).Contains(lm.ReaderNumber)).ToListAsync();
        }
    }
}
