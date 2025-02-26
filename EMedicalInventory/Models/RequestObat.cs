using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EMedicalInventory.Models
{
    public class RequestObat
    {
        [Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

        [Required]
        public string UserId { get; set; }  // ID User yang melakukan request (dari Identity)

        [Required]
        public int MedicineId { get; set; }

        [Required]
        public int Quantity { get; set; }

		public string CreatedBy { get; set; }
		[Required]
		public DateTime CreatedDate { get; set; }
		public string? UpdatedBy { get; set; }
		public DateTime? UpdatedDate { get; set; }

		[Required]
        public string Status { get; set; } = "Pending"; // Pending, Approved, Rejected

        [ForeignKey("MedicineId")]
        public Obat Obat { get; set; }

        [ForeignKey("UserId")]
        public Users User { get; set; }
    }
}
