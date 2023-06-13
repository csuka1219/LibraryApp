using System.Net.Http.Json;
using Library;

namespace ReaderClient.Services
{
    public class LoanService : ILoanService
    {
        private readonly HttpClient httpClient;

        public LoanService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public Task<List<Loan>?> GetAllLoanAsync() =>
            httpClient.GetFromJsonAsync<List<Loan>?>("Loan");

        public Task<Loan?> GetLoanByIdAsync(int id) =>
            httpClient.GetFromJsonAsync<Loan?>($"Loan/{id}");


    }
}
