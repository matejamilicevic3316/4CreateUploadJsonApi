using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarStoreDatabaseAccess.Configurations
{
    public class TrialConfiguration : IEntityTypeConfiguration<Trial>
    {
        public void Configure(EntityTypeBuilder<Trial> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Title).IsRequired();
            builder.Property(x => x.StartDate).IsRequired();
            builder.Property(x => x.Status).IsRequired();
        }
    }
}
