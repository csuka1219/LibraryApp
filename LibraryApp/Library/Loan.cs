using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
	public class Loan
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[ForeignKey("Book")]
		public int ReaderNumber { get; set; }

		[ForeignKey("LibraryMember")]
		public int InvNumber { get; set; }
		public DateTime LoanDate { get; set; }

		[Compare(nameof(LoanDate), ErrorMessage = "Return deadline must be later than the loan date.")]
		public DateTime returnDeadline{ get; set; }

		public virtual ICollection<Book> Books { get; set; }
		public virtual ICollection<LibraryMember> LibraryMembers { get; set; }
	}
}
