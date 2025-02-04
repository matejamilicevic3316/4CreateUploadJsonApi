using _4CreateWebApiJsonUpload.Configurations;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace _4CreateWebApiJsonUpload
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
