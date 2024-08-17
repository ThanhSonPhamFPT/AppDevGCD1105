using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models
{
	public class BookVM
	{
		public Book Book { get; set; }
		[ValidateNever]
		public IEnumerable<SelectListItem> Categories { get; set; }
	}
}
