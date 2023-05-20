using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
	internal class Loan
	{
		public int ReaderNumber { get; set; }
		public int InvNumber { get; set; }
		public DateTime LoanDate { get; set; }

		[Compare(nameof(LoanDate), ErrorMessage = "Return deadline must be later than the loan date.")]
		public DateTime returnDeadline{ get; set; }
	}
}
