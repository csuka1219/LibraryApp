using LibrarianClient.Services;
using Library;

namespace LibrarianClientTests
{
	[TestClass]
	public class LibraryMemberServiceUnitTests
	{
        HttpClient client = new HttpClient();
		[TestMethod]
        public async Task AddNewMember_ValidData_Success()
        {
            client.BaseAddress = new Uri("https://localhost:7096/api/");
            LibraryMemberService memberService = new LibraryMemberService(client);
            LibraryMember libraryMember = new LibraryMember
            {
                Name = "JohnDoe",
                Address = "123 Main St",
                BirthDate = new DateTime(1990, 5, 10)
            };

            int result = await memberService.AddLibraryMemberAsync(libraryMember);
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public async Task AddNewMember_InValidData_Success()
        {
            client.BaseAddress = new Uri("https://localhost:7096/api/");
            LibraryMemberService memberService = new LibraryMemberService(client);
            LibraryMember libraryMember = new LibraryMember
            {
                Name = "John Doe",
                Address = "123 Main St",
                BirthDate = new DateTime(1990, 5, 10)
            };

            int result = await memberService.AddLibraryMemberAsync(libraryMember);
            Assert.IsTrue(result == 0);
        }
    }
}