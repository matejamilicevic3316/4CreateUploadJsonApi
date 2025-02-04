using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _4CreateWebApiJsonUpload.Configurations
{
    public class TrialConfiguration : IEntityTypeConfiguration<Trial>
    {
        public void Configure(EntityTypeBuilder<Trial> builder)
        {
            builder.HasKey(t => t.TrialId);
            builder.Property(x => x.TrialId).IsRequired();
            builder.Property(x => x.Title).IsRequired();
            builder.Property(x => x.StartDate).IsRequired();
            builder.Property(x => x.Status).IsRequired();
        }
    }
}
