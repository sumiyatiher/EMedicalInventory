using EMedicalInventory.ViewModels.Obat;

namespace EMedicalInventory.ViewModels.Request
{
	public class RequestObatListViewModel
	{
		public IEnumerable<RequestObatViewModel> requests { get; set; }
		public string? SearchTerm { get; set; }
		public string? SortColumn { get; set; }
		public string? SortOrder { get; set; }
		public int? CurrentPage { get; set; }
		public int? TotalPages { get; set; }
	}
}
