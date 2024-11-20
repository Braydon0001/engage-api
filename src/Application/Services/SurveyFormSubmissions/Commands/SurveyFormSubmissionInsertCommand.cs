namespace Engage.Application.Services.SurveyFormSubmissions.Commands;

public class SurveyFormSubmissionInsertCommand : IMapTo<SurveyFormSubmission>, IRequest<SurveyFormSubmission>
{
    public int? EmployeeId { get; init; }
    public int? UserId { get; init; }
    public int SurveyFormId { get; init; }
    public int? StoreId { get; init; }
    public string SubmissionUuid { get; init; }
    public DateTime StartedDate { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormSubmissionInsertCommand, SurveyFormSubmission>();
    }
}

public record SurveyFormSubmissionInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormSubmissionInsertCommand, SurveyFormSubmission>
{
    public async Task<SurveyFormSubmission> Handle(SurveyFormSubmissionInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<SurveyFormSubmissionInsertCommand, SurveyFormSubmission>(command);

        Context.SurveyFormSubmissions.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class SurveyFormSubmissionInsertValidator : AbstractValidator<SurveyFormSubmissionInsertCommand>
{
    public SurveyFormSubmissionInsertValidator()
    {
        RuleFor(e => e.EmployeeId);
        RuleFor(e => e.UserId);
        RuleFor(e => e.SurveyFormId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.StoreId);
    }
}