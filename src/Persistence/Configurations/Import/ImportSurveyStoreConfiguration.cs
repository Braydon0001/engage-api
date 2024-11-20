using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Engage.Persistence.Configurations.Import
{
    public class ImportSurveyStoreConfiguration : IEntityTypeConfiguration<ImportSurveyStore>
    {
        public void Configure(EntityTypeBuilder<ImportSurveyStore> builder)
        {
            builder.HasIndex(e => new { e.ImportFileId, e.RowNo })
                .IsUnique();

            builder.Property(e => e.ImportRowMessage)
                .HasMaxLength(1000);

            builder.Property(e => e.AccountNumber)
                .HasMaxLength(1000);
        }
    }
}
