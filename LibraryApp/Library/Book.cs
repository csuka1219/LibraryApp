using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library
{
	public class Book
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int InvNumber { get; set; }

		[Required]
		[MinLength(1)]
		public string Title { get; set; }
		public string? Author { get; set; }
		public string? Publisher { get; set; }
		public int Year { get; set; }
	}

}