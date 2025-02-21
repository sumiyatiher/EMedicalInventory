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

        public async Task<ObatListViewModel> GetAllDataAsync(string searchTerm, string sortColumn, string sortOrder, int page, int pageSize)
        {
            var query = _dbContext.Obat.AsQueryable();

            //Filtering
            if (!String.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(o => o.DrugName.Contains(searchTerm));
            }

            //Sorting
            sortColumn = string.IsNullOrEmpty(sortColumn) ? "DrugName" : sortColumn;
            sortOrder = string.IsNullOrEmpty(sortOrder) ? "asc" : sortOrder;

            query = sortColumn switch
            {
                "Stock" => sortOrder == "asc" ? query.OrderBy(o => o.Stock) : query.OrderByDescending(o => o.Stock),
                "Price" => sortOrder == "asc" ? query.OrderBy(o => o.Price) : query.OrderByDescending(o => o.Price),
                "ExpiredDate" => sortOrder == "asc" ? query.OrderBy(o => o.ExpiredDate) : query.OrderByDescending(o => o.ExpiredDate),
                _ => sortOrder == "asc" ? query.OrderBy(o => o.DrugName) : query.OrderByDescending(o => o.DrugName),
            };

            //Pagination
            int totalItems = query.Count();
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var obats = query.Skip((page - 1) * pageSize).Take(pageSize)
                .Select(o => new ObatViewModel
                {
                    Id = o.Id,
                    DrugName = o.DrugName,
                    Stock = o.Stock,
                    Price = o.Price,
                    ExpiredDate = o.ExpiredDate
                }).ToList();

            return new ObatListViewModel
            {
                Obats = obats,
                SearchTerm = searchTerm,
                SortColumn = sortColumn,
                SortOrder = sortOrder,
                CurrentPage = page,
                TotalPages = totalPages
            };
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
