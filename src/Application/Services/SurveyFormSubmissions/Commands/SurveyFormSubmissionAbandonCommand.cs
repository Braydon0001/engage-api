namespace Engage.Application.Services.SurveyFormSubmissions.Commands;

public class SurveyFormSubmissionAbandonCommand : IMapTo<SurveyFormSubmission>, IRequest<SurveyFormSubmission>
{
    public int? SurveyFormSubmissionId { get; set; }
    public int EmployeeId { get; init; }
    public int SurveyFormId { get; init; }
    public int? StoreId { get; init; }
    public string SubmissionUuid { get; init; }
    public DateTime AbandonedDate { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormSubmissionAbandonCommand, SurveyFormSubmission>();
    }
}

public record SurveyFormSubmissionAbandonHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormSubmissionAbandonCommand, SurveyFormSubmission>
{
    public async Task<SurveyFormSubmission> Handle(SurveyFormSubmissionAbandonCommand command, CancellationToken cancellationToken)
    {
        SurveyFormSubmission entity;
        if (command.SurveyFormSubmissionId == null)
        {
            entity = await Context.SurveyFormSubmissions.SingleOrDefaultAsync(e => e.SubmissionUuid == command.SubmissionUuid, cancellationToken);
        }
        else
        {
            entity = await Context.SurveyFormSubmissions.SingleOrDefaultAsync(e => e.SurveyFormSubmissionId == command.SurveyFormSubmissionId, cancellationToken);
        }

        if (entity == null)
        {
            return null;
        }

        entity.IsAbandoned = true;
        entity.AbandonedDate = command.AbandonedDate;

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class SurveyFormSubmissionAbandonValidator : AbstractValidator<SurveyFormSubmissionAbandonCommand>
{
    public SurveyFormSubmissionAbandonValidator()
    {
        RuleFor(x => x.SurveyFormSubmissionId);
        RuleFor(e => e.EmployeeId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.SurveyFormId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.StoreId);
        RuleFor(e => e.SubmissionUuid).NotEmpty().Unless(e => e.SurveyFormSubmissionId.HasValue);
        RuleFor(e => e.AbandonedDate).NotEmpty();
    }
}