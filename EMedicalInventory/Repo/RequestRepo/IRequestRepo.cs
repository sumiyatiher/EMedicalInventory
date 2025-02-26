using EMedicalInventory.ViewModels.Request;

namespace EMedicalInventory.Repo.RequestRepo
{
    public interface IRequestRepo
    {
        Task<RequestObatListViewModel> GetRequestObatListViewModelAsync(string searchTerm, string sortColumn, string sortOrder, int page, int pageSize, string userid);
		Task CreateRequestAsync(RequestObatViewModel model, string userId);
	}
}
