namespace Engage.Persistence.Configurations;

public class SurveyInstanceConfiguration : IEntityTypeConfiguration<SurveyInstance>
{
    public void Configure(EntityTypeBuilder<SurveyInstance> builder)
    {
        builder.HasIndex(e => new { e.EmployeeId, e.StoreId, e.SurveyId, e.SurveyDate })
            .IsUnique();

        builder.HasOne(x => x.Employee)
            .WithMany(s => s.SurveyInstances)
            .HasForeignKey(x => x.EmployeeId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(x => x.Store)
            .WithMany(s => s.SurveyInstances)
            .HasForeignKey(x => x.StoreId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(x => x.Survey)
            .WithMany(s => s.SurveyInstances)
            .HasForeignKey(x => x.SurveyId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.Property(e => e.SurveyDate)
            .HasColumnType("date");

        builder.Property(e => e.Note)
            .HasMaxLength(5000);

        builder.Property(e => e.IsCompleted);
    }
}

public class SurveyAnswerConfiguration : IEntityTypeConfiguration<SurveyAnswer>
{
    public void Configure(EntityTypeBuilder<SurveyAnswer> builder)
    {
        builder.HasIndex(e => new { e.SurveyInstanceId, e.SurveyQuestionId })
            .IsUnique();

        builder.Property(x => x.Answer)
         .HasMaxLength(5000);

        builder.HasOne(x => x.SurveyInstance)
            .WithMany(s => s.SurveyAnswers)
            .HasForeignKey(x => x.SurveyInstanceId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.Property(e => e.Files).HasColumnType("json");
    }
}

public class SurveyAnswerOptionConfiguration : IEntityTypeConfiguration<SurveyAnswerOption>
{
    public void Configure(EntityTypeBuilder<SurveyAnswerOption> builder)
    {
        builder.HasIndex(e => new { e.SurveyAnswerId, e.SurveyAnswerOptionId })
            .IsUnique();

        builder.HasOne(x => x.SurveyAnswer)
            .WithMany(s => s.SurveyAnswerOptions)
            .HasForeignKey(x => x.SurveyAnswerId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(x => x.SurveyQuestionOption)
            .WithMany(s => s.SurveyAnswerOptions)
            .HasForeignKey(x => x.SurveyQuestionOptionId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}

public class SurveyAnswerPhotoConfiguration : IEntityTypeConfiguration<SurveyAnswerPhoto>
{
    public void Configure(EntityTypeBuilder<SurveyAnswerPhoto> builder)
    {
        builder.HasOne(x => x.SurveyAnswer)
            .WithMany(s => s.SurveyAnswerPhotos)
            .HasForeignKey(x => x.SurveyAnswerId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.Property(e => e.FileName)
            .IsRequired();


        builder.Property(e => e.Folder)
            .IsRequired();

    }
}
