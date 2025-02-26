using AutoMapper;
using EMedicalInventory.Data;
using EMedicalInventory.Models;
using EMedicalInventory.ViewModels.Obat;
using EMedicalInventory.ViewModels.Request;
using Microsoft.EntityFrameworkCore;

namespace EMedicalInventory.Repo.RequestRepo
{
	public class RequestRepo : IRequestRepo
	{
		private readonly MedicalInventoryDBContext _dbContext;
		private readonly IMapper _mapper;

		public RequestRepo(MedicalInventoryDBContext context, IMapper mapper)
		{
			_dbContext = context;
			_mapper = mapper;
		}

		public async Task CreateRequestAsync(RequestObatViewModel model, string userId)
		{
			var request = _mapper.Map<RequestObat>(model);

			request.Status = "Pending";
			request.CreatedBy = userId;
			request.CreatedDate = DateTime.Now;
			request.UpdatedBy = userId;
			request.UpdatedDate = DateTime.Now;

			await _dbContext.RequestObat.AddAsync(request);
			await _dbContext.SaveChangesAsync();

			var VMObat = _mapper.Map<RequestObatViewModel>(request);

		}

		public Task<RequestObatListViewModel> GetRequestObatListViewModelAsync(string searchTerm, string sortColumn, string sortOrder, int page, int pageSize,string userid)
		{
			var query = _dbContext.RequestObat
						.Include(o => o.Obat)
						.Include(r => r.User)
						.AsQueryable();

			query = query.Where(x => x.UserId == userid);

			//Filtering
			if (!String.IsNullOrEmpty(searchTerm))
			{
				query = query.Where(r => r.Obat.DrugName.Contains(searchTerm) || r.User.UserName.Contains(searchTerm));
			}

			// Sorting
			switch (sortColumn)
			{
				case "DrugName":
					query = sortOrder == "desc" ? query.OrderByDescending(r => r.Obat.DrugName) : query.OrderBy(r => r.Obat.DrugName);
					break;
				case "Quantity":
					query = sortOrder == "desc" ? query.OrderByDescending(r => r.Quantity) : query.OrderBy(r => r.Quantity);
					break;
				case "Status":
					query = sortOrder == "desc" ? query.OrderByDescending(r => r.Status) : query.OrderBy(r => r.Status);
					break;
				case "UserName":
					query = sortOrder == "desc" ? query.OrderByDescending(r => r.User.UserName) : query.OrderBy(r => r.User.UserName);
					break;
				default:
					query = query.OrderByDescending(r => r.CreatedDate); // Default sorting berdasarkan waktu dibuat
					break;
			}

			//Pagination
			int totalItems = query.Count();
			int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

			var request = query.Skip((page - 1) * pageSize).Take(pageSize)
				.Select(r => new RequestObatViewModel
				{
					Id = r.Id,
					MedicineId = r.MedicineId,
					MedicineName = r.Obat.DrugName,
					Quantity = r.Quantity,
					Status = r.Status,
					UserId = r.UserId,
					UserName = r.User.UserName,
				}).ToList();

			var result =  new RequestObatListViewModel
			{
				requests = request,
				SearchTerm = searchTerm,
				SortColumn = sortColumn,
				SortOrder = sortOrder,
				CurrentPage = page,
				TotalPages = totalPages
			};

			return Task.FromResult(result);

		}
	}
}
