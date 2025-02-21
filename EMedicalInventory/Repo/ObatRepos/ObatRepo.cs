using AutoMapper;
using EMedicalInventory.Data;
using EMedicalInventory.Models;
using EMedicalInventory.ViewModels.Obat;
using Microsoft.AspNetCore.Http.HttpResults;
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

        public async Task DeleteAsync(int id)
        {
            var obat = await _dbContext.Obat.FirstOrDefaultAsync(o => o.Id == id);
            if(obat != null)
            {
                _dbContext.Remove(obat);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Drug Not Found!");
            }
        }

        public Task<List<Obat>> GetAllDataAsync()
        {
            var obatList = _dbContext.Obat.ToListAsync();
            return obatList;
        }

        

        public async Task UpdateAsync(ObatViewModel model,string userid)
        {
            var obatExist = await _dbContext.Obat.FirstOrDefaultAsync(o => o.Id == model.Id);
            if (obatExist != null) 
            {
                // Update properties
                obatExist.DrugName = model.DrugName;
                obatExist.Stock = model.Stock;
                obatExist.Price = model.Price;
                obatExist.ExpiredDate = model.ExpiredDate;
                obatExist.UpdatedBy = userid;
                obatExist.UpdatedDate = DateTime.Now;

                _dbContext.Obat.Update(obatExist);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<ObatViewModel> GetByIdAsync(int id)
        {
            var obat = await _dbContext.Obat.FirstOrDefaultAsync(o => o.Id == id);
            if (obat != null)
            {
               return _mapper.Map<ObatViewModel>(obat);
            }
            return null;
        }
    }
}
