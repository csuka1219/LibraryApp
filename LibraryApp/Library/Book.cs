using System.ComponentModel.DataAnnotations;

namespace Library
{
	public class Book
	{
		[Required]
		[MinLength(1)]
		public string Title { get; set; }
		public string? Author { get; set; }
		public string? Publisher { get; set; }
		public int InvNumber { get; set; }
		public int Year { get; set; }

	}

}