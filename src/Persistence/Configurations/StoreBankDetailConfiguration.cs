using Engage.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Engage.Persistence.Configurations
{
    public class StoreBankDetailConfiguration : IEntityTypeConfiguration<StoreBankDetail>
    {
        public void Configure(EntityTypeBuilder<StoreBankDetail> builder)
        {
            builder.Property(e => e.Bank)
                .IsRequired()
                .HasMaxLength(120);

            builder.Property(e => e.BranchCode)
               .IsRequired()
               .HasMaxLength(30);

            builder.Property(e => e.AccountNumber)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(e => e.AccountHolder)
                .IsRequired()
                .HasMaxLength(200);

            //builder.Property(e => e.Note)
            //  .HasColumnType("ntext");

            builder.HasOne(x => x.Store)
                .WithMany(s => s.BankDetails)
                .HasForeignKey(x => x.StoreId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
