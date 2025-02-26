using EMedicalInventory.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMedicalInventory.ViewModels.Request
{
    public class RequestObatViewModel
    {
		public int Id { get; set; }
		public int MedicineId { get; set; }
		public string? MedicineName { get; set; }

		[Required(ErrorMessage = "Quantity have more than 0!")]
		public int Quantity { get; set; }

		[Required(ErrorMessage ="Status is required!")]
		public string Status { get; set; }
		public string? UserId { get; set; }
		public string? UserName { get; set; }

	}
}
