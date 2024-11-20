namespace Engage.Persistence.Configurations
{
    public class VacancyConfiguration : IEntityTypeConfiguration<Vacancy>
    {
        public void Configure(EntityTypeBuilder<Vacancy> builder)
        {
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(120);

            builder.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(1000);
        }
    }
}
