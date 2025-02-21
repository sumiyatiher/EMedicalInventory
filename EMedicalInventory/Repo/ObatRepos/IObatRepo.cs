using EMedicalInventory.Models;
using EMedicalInventory.ViewModels.Obat;

namespace EMedicalInventory.Repo.ObatRepos
{
    public interface IObatRepo
    {
        Task<List<Obat>> GetAllDataAsync();
        Task AddObatAsync(ObatViewModel model,string userid);
        Task<ObatViewModel> GetByIdAsync(int id);
        Task UpdateAsync(ObatViewModel model,string userid);
        Task DeleteAsync(int id);
    }
}
