using CarStoreDatabaseAccess.Configurations;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace CarStoreDatabaseAccess
{
    public class MedicineContext : DbContext
    {
        public MedicineContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Trial> Trials { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TrialConfiguration());
        }
    }
}
