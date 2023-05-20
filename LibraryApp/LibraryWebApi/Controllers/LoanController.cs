using Library;
using LibraryWebApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoanController : Controller
    {
        private readonly ILoanRepository _loanRepository;

        public LoanController(ILoanRepository loanRepository)
        {
            _loanRepository = loanRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Loan>>> GetAllLoans()
        {
            var loans = await _loanRepository.GetAllLoansAsync();
            return Ok(loans);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Loan>> GetLoanById(int id)
        {
            var loan = await _loanRepository.GetLoanByIdAsync(id);
            if (loan is null)
            {
                return NotFound();
            }

            return Ok(loan);
        }

        [HttpPost]
        public async Task<ActionResult<Loan>> AddLoan(Loan loan)
        {
            if (loan.LoanDate >= loan.returnDeadline)
            {
                return BadRequest("Return deadline must be later than or equal to the loan date");
            }

            await _loanRepository.AddLoanAsync(loan);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLoan(int id, Loan loan)
        {
            if (id != loan.Id)
            {
                return BadRequest();
            }

            await _loanRepository.UpdateLoanAsync(loan);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLoan(int id)
        {
            var loan = await _loanRepository.GetLoanByIdAsync(id);
            if (loan is null)
            {
                return NotFound();
            }

            await _loanRepository.DeleteLoanAsync(loan);
            return NoContent();
        }
    }
}
