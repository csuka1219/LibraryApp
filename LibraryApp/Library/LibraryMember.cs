using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
	internal class LibraryMember
	{
		[Required(ErrorMessage = "Name is required")]
		[RegularExpression(@"^[^\s!?_-:;#]+$", ErrorMessage = "Name cannot contain special characters")]
		public string Name { get; set; }
		public string Address { get; set; }
		public int ReaderNumber { get; set; }

		[Range(typeof(DateTime), "1900-01-01", "2020-12-31")]
		public DateTime BirthDate { get; set; }

	}
}
