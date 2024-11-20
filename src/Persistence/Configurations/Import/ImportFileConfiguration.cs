using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Engage.Persistence.Configurations.Import
{
    public class ImportFileConfiguration : IEntityTypeConfiguration<ImportFile>
    {
        public void Configure(EntityTypeBuilder<ImportFile> builder)
        {
            builder.Property(e => e.FileName)
                    .IsRequired()
                    .HasMaxLength(1000);
        }
    }
}
