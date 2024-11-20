namespace Engage.Persistence.Configurations;

public class ContactConfiguration : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.Property(e => e.FullName)
            .IsRequired()
            .HasMaxLength(160);

        builder.HasOne(x => x.Stakeholder)
            .WithMany(s => s.Contacts)
            .HasForeignKey(x => x.StakeholderId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(x => x.PrimaryEmailContactItem)
            .WithMany(s => s.PrimaryEmailContactItems)
            .HasForeignKey(x => x.PrimaryEmailContactItemId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(x => x.PrimaryMobileContactItem)
            .WithMany(s => s.PrimaryMobileContactItems)
            .HasForeignKey(x => x.PrimaryMobileContactItemId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}

public class ContactItemConfiguration : IEntityTypeConfiguration<ContactItem>
{
    public void Configure(EntityTypeBuilder<ContactItem> builder)
    {
        builder.Property(e => e.Value)
            .IsRequired()
            .HasMaxLength(260);

        builder.HasOne(x => x.Contact)
            .WithMany(s => s.ContactItems)
            .HasForeignKey(x => x.ContactId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}

public class ContactEventConfiguration : IEntityTypeConfiguration<ContactEvent>
{
    public void Configure(EntityTypeBuilder<ContactEvent> builder)
    {
        builder.HasOne(x => x.Contact)
            .WithMany(s => s.ContactEvents)
            .HasForeignKey(x => x.ContactId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
