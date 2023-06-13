using Library;

namespace ReaderClient.Services
{
    public interface ILoanService
    {
        Task<List<Loan>?> GetAllLoanAsync();

        Task<Loan?> GetLoanByIdAsync(int id);


    }
}
