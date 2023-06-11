using LibrarianClient.Services;
using Library;

namespace LibrarianClientTests
{
	[TestClass]
	public class BookServiceUnitTests
	{
        HttpClient client = new HttpClient();
		[TestMethod]
        public async Task AddNewBook_ValidData_Success()
        {
            client.BaseAddress = new Uri("https://localhost:7096/api/");
            BookService bookService = new BookService(client);
            Book book = new Book
            {
                Title = "TestTitle",
                Author = "TestAuthor",
                Publisher = "TestPublisher",
                Year = 1990
            };

            int result = await bookService.AddBookAsync(book);
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public async Task AddNewBook_InValidData_Success()
        {
            client.BaseAddress = new Uri("https://localhost:7096/api/");
            BookService bookService = new BookService(client);
            Book book = new Book
            {
                Title = "",
                Author = "TestAuthor",
                Publisher = "TestPublisher",
                Year = 1990
            };

            int result = await bookService.AddBookAsync(book);
            Assert.IsTrue(result == 0);
        }
    }
}