using System.ComponentModel.DataAnnotations;

namespace KoncarTaskTriangles.Models.ViewModels
{
	public class TriangleViewModel
	{
		public int UserId { get; set; }
		[Required(ErrorMessage = "Ax is required.")]
		[Display(Name = "Ax")]
		public int Ax { get; set; }
		[Required(ErrorMessage = "Ay is required.")]
		public int Ay { get; set; }
		[Required(ErrorMessage = "Bx is required.")]
		public int Bx { get; set; }
		[Required(ErrorMessage = "By is required.")]
		public int By { get; set; }
		[Required(ErrorMessage = "Cx is required.")]
		public int Cx { get; set; }
		[Required(ErrorMessage = "Cy is required.")]
		public int Cy { get; set; }
	}
}
