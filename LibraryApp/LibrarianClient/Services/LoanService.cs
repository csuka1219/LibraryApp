using System.Net.Http.Json;
using Library;

namespace LibrarianClient.Services
{
    public class LoanService: ILoanService
	{
        private readonly HttpClient _httpClient;

        public LoanService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<List<Loan>?> GetAllLoanAsync() =>
            _httpClient.GetFromJsonAsync<List<Loan>>("Loan");

        public Task<Loan?> GetLoanByIdAsync(int id) =>
            _httpClient.GetFromJsonAsync<Loan?>($"Loan/{id}");

        public async Task UpdateLoanAsync(int id, Loan person) =>
            await _httpClient.PutAsJsonAsync($"Loan/{id}", person);

        public async Task DeleteLoanAsync(int id) =>
            await _httpClient.DeleteAsync($"Loan/{id}");

        public async Task AddLoanAsync(Loan person) =>
            await _httpClient.PostAsJsonAsync("Loan", person);
    }
}
