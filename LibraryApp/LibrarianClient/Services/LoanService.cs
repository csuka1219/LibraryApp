using System.Net.Http.Json;
using System.Text.Json;
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

        public async Task<int> AddLoanAsync(Loan person) 
        {
            HttpResponseMessage response;
            try
            {
                response = await httpClient.PostAsJsonAsync("Book", person);
                return await response.Content.ReadFromJsonAsync<int>();
            }
            catch (JsonException)
            {
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }
    }
}
