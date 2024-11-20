using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Engage.Persistence.Configurations.Import
{
    public class ImportPromotionStoreConfiguration : IEntityTypeConfiguration<ImportPromotionStore>
    {
        public void Configure(EntityTypeBuilder<ImportPromotionStore> builder)
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
