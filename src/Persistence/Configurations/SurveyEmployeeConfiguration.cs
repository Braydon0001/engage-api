namespace Engage.Persistence.Configurations;

public class SurveyEmployeeConfiguration : IEntityTypeConfiguration<SurveyEmployee>
{
    public void Configure(EntityTypeBuilder<SurveyEmployee> builder)
    {
        builder.HasKey(e => new { e.SurveyId, e.EmployeeId })
            .IsClustered(false);

        builder.HasOne(x => x.Survey)
            .WithMany(s => s.SurveyEmployees)
            .HasForeignKey(x => x.SurveyId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(x => x.Employee)
            .WithMany(s => s.SurveyEmployees)
            .HasForeignKey(x => x.EmployeeId)
            .OnDelete(DeleteBehavior.ClientSetNull);

    }
}
