using Library;
using System.ComponentModel.DataAnnotations;

namespace LibraryWebApiTests
{
	[TestClass]
	public class LibraryMemberUnitTests
	{
		[TestMethod]
		public void LibraryMember_ValidName_NoValidationErrors()
		{
			var libraryMember = new LibraryMember
			{
				ReaderNumber = 1,
				Name = "JohnDoe",
				Address = "123 Main St",
				BirthDate = new DateTime(1990, 5, 10)
			};

			var validationContext = new ValidationContext(libraryMember);

			var validationResultList = new List<ValidationResult>();
			var isValid = Validator.TryValidateObject(libraryMember, validationContext, validationResultList, true);
			
			Assert.IsTrue(isValid);
			Assert.IsTrue(validationResultList.Count == 0);
		}


		[TestMethod]
		public void LibraryMember_InvalidName_ValidationErrors()
		{
			var libraryMember = new LibraryMember
			{
				ReaderNumber = 1,
				Name = "John Doe",
				Address = "123 Main St",
				BirthDate = new DateTime(1990, 5, 10)
			};

			var validationContext = new ValidationContext(libraryMember);

			var validationResultList = new List<ValidationResult>();
			var isValid = Validator.TryValidateObject(libraryMember, validationContext, validationResultList, false);

			Assert.IsTrue(isValid);
			Assert.IsTrue(validationResultList.Count == 0);
		}
	}
}