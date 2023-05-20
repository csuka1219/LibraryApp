using Library;
using LibraryWebApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LibraryMemberController : Controller
    {
        private readonly ILibraryMemberRepository _libraryMemberRepository;

        public LibraryMemberController(ILibraryMemberRepository libraryMemberRepository)
        {
            _libraryMemberRepository = libraryMemberRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<LibraryMember>>> GetAllLibraryMembers()
        {
            var members = await _libraryMemberRepository.GetAllLibraryMembersAsync();
            return Ok(members);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LibraryMember>> GetLibraryMemberById(int id)
        {
            var member = await _libraryMemberRepository.GetLibraryMemberByIdAsync(id);
            if (member is null)
            {
                return NotFound();
            }

            return Ok(member);
        }

        [HttpPost]
        public async Task<ActionResult<LibraryMember>> AddLibraryMember(LibraryMember member)
        {
            await _libraryMemberRepository.AddLibraryMemberAsync(member);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLibraryMember(int id, LibraryMember member)
        {
            if (id != member.ReaderNumber)
            {
                return BadRequest();
            }

            await _libraryMemberRepository.UpdateLibraryMemberAsync(member);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLibraryMember(int id)
        {
            var member = await _libraryMemberRepository.GetLibraryMemberByIdAsync(id);
            if (member is null)
            {
                return NotFound();
            }

            await _libraryMemberRepository.DeleteLibraryMemberAsync(member);
            return NoContent();
        }
    }
}
