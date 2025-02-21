using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EMedicalInventory.ViewModels.Obat
{
    public class ObatViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name of Drug is required!")]
        public string DrugName { get; set; }
        [Required(ErrorMessage = "Stock is required!")]
        public int Stock { get; set; }
        [Required(ErrorMessage = "Price is required!")]
        [Column(TypeName ="decimal(18,2)")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Expired Date is required!")]
        public DateTime ExpiredDate { get; set; }

    }
}
