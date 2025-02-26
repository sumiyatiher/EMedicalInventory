using EMedicalInventory.Models;
using Microsoft.EntityFrameworkCore;

namespace EMedicalInventory.Data
{
    public class MedicalInventoryDBContext : DbContext
    {
        public MedicalInventoryDBContext(DbContextOptions<MedicalInventoryDBContext> options) : base(options) { }

        public DbSet<Obat> Obat { get; set; }
        public DbSet<RequestObat> RequestObat { get; set; }
    }
}
