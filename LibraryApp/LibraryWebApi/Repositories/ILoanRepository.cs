using Library;

namespace LibraryWebApi.Repositories
{
    public interface ILoanRepository
    {
        Task<List<Loan>> GetAllLoansAsync();

        Task<Loan> GetLoanByIdAsync(int id);

        Task AddLoanAsync(Loan loan);

        Task UpdateLoanAsync(Loan loan);

        Task DeleteLoanAsync(Loan loan);
    }
}
