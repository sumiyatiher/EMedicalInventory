using EMedicalInventory.Models;
using EMedicalInventory.ViewModels.Obat;

namespace EMedicalInventory.Repo.ObatRepos
{
    public interface IObatRepo
    {
        Task<ObatListViewModel> GetAllDataAsync(string searchTerm, string sortColumn, string sortOrder, int page, int pageSize);
        Task AddObatAsync(ObatViewModel model,string userid);
        Task<ObatViewModel> GetByIdAsync(int id);
        Task UpdateAsync(ObatViewModel model,string userid);
        Task DeleteAsync(int id);
    }
}
