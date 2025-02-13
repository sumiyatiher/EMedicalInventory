using EMedicalInventory.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EMedicalInventory.Data
{
    public class AppDBContext : IdentityDbContext<Users>
    {
        public AppDBContext(DbContextOptions options) : base(options)
        {
                
        }

		
	}
}
