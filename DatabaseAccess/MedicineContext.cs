using CarStoreDatabaseAccess.Configurations;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace CarStoreDatabaseAccess
{
    public class MedicineContext : DbContext
    {

        public DbSet<Trial> Trials { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(LocalDb)\MSSQLLocalDb;Initial Catalog=CarStoreDb;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TrialConfiguration());
        }
    }
}
