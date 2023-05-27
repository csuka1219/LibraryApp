using System.Net.Http.Json;
using Library;

namespace LibrarianClient.Services
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

        public async Task UpdateLoanAsync(int id, Loan person) =>
            await httpClient.PutAsJsonAsync($"Loan/{id}", person);

        public async Task DeleteLoanAsync(int id) =>
            await httpClient.DeleteAsync($"Loan/{id}");

        public async Task AddLoanAsync(Loan person) =>
            await httpClient.PostAsJsonAsync("Loan", person);
    }
}
