using Library;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;

namespace LibraryWebApi.Data
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public virtual DbSet<Book> Books { get; set; }

        public virtual DbSet<LibraryMember> LibraryMembers { get; set; }

        public virtual DbSet<Loan> Loans { get; set; }

    }
}
