using Library;
using LibraryWebApi.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebApi.Repositories
{
    public class LoanRepository:ILoanRepository
    {
        private readonly LibraryDbContext _context;

        public LoanRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<List<Loan>> GetAllLoansAsync()
        {
            return await _context.Loans.ToListAsync();
        }

        public async Task<Loan> GetLoanByIdAsync(int id)
        {
            return await _context.Loans.FindAsync(id);
        }

        public async Task AddLoanAsync(Loan loan)
        {
            _context.Loans.Add(loan);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateLoanAsync(Loan loan)
        {
            _context.Entry(loan).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteLoanAsync(Loan loan)
        {
            _context.Loans.Remove(loan);
            await _context.SaveChangesAsync();
        }
    }
}
