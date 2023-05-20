using Library;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryWebApiTests
{
	[TestClass]
	public class LoanUnitTests
	{
		[TestMethod]
		public void Loan_ValidDate_NoValidationErrors()
		{
			var loan = new Loan
			{
				ReaderNumber = 1,
				InvNumber = 1,
				LoanDate = new DateTime(1990, 5, 10),
				returnDeadline = new DateTime(1991, 5, 10)
			};

			var validationContext = new ValidationContext(loan);

			var validationResultList = new List<ValidationResult>();
			var isValid = Validator.TryValidateObject(loan, validationContext, validationResultList, true);

			Assert.IsTrue(isValid);
			Assert.IsTrue(validationResultList.Count == 0);
		}

		[TestMethod]
		public void Loan_InvalidDate_ValidationErrors()
		{
			var loan = new Loan
			{
				ReaderNumber = 1,
				InvNumber = 1,
				LoanDate = new DateTime(1990, 5, 10),
				returnDeadline = new DateTime(1989, 5, 10)
			};

			var validationContext = new ValidationContext(loan);

			
			var validationResultList = new List<ValidationResult>();
			var isValid = Validator.TryValidateObject(loan, validationContext, validationResultList, false);

			Assert.IsTrue(isValid);
			Assert.IsTrue(validationResultList.Count == 0);
		}

	}
}
