using Library;

namespace LibrarianClient.Services
{
    public interface ILoanService
    {
        Task<List<Loan>?> GetAllLoanAsync();

        Task<Loan?> GetLoanByIdAsync(int id);

        Task UpdateLoanAsync(int id, Loan person);

        Task DeleteLoanAsync(int id);

        Task<int> AddLoanAsync(Loan person);
    }
}
