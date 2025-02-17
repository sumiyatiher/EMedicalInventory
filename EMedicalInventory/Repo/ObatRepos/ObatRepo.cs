using AutoMapper;
using EMedicalInventory.Data;
using EMedicalInventory.Models;
using EMedicalInventory.ViewModels.Obat;
using Microsoft.EntityFrameworkCore;

namespace EMedicalInventory.Repo.ObatRepos
{
    public class ObatRepo : IObatRepo
    {
        private readonly MedicalInventoryDBContext _dbContext;
        private readonly IMapper _mapper;

        public ObatRepo(MedicalInventoryDBContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }
       

        public async Task AddObatAsync(ObatViewModel model, string userid)
        {
            var obat = _mapper.Map<Obat>(model);

            obat.CreatedBy = userid;
            obat.CreatedDate = DateTime.Now;
            obat.UpdatedBy = userid;
            obat.UpdatedDate = DateTime.Now;

            await _dbContext.Obat.AddAsync(obat);
            await _dbContext.SaveChangesAsync(); 

            var VMObat = _mapper.Map<ObatViewModel>(obat);

        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Obat>> GetAllDataAsync()
        {
            var obatList = _dbContext.Obat.ToListAsync();
            return obatList;
        }

        

        public Task UpdateAsync(ObatViewModel model)
        {
            throw new NotImplementedException();
        }

        Task<Obat> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
