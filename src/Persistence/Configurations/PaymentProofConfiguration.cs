namespace Engage.Persistence.Configurations;

public class PaymentProofConfiguration : IEntityTypeConfiguration<PaymentProof>
{
    public void Configure(EntityTypeBuilder<PaymentProof> builder)
    {
        builder.Property(e => e.PaymentProofId).IsRequired();
        builder.Property(e => e.Files).HasColumnType("json");
    }
}