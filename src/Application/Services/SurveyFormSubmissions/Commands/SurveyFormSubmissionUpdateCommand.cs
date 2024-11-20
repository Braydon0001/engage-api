namespace Engage.Application.Services.SurveyFormSubmissions.Commands;

public class SurveyFormSubmissionUpdateCommand : IMapTo<SurveyFormSubmission>, IRequest<SurveyFormSubmission>
{
    public int? Id { get; set; }
    public int? EmployeeId { get; init; }
    public int? UserId { get; init; }
    public int SurveyFormId { get; init; }
    public int? StoreId { get; init; }
    public string SubmissionUuid { get; init; }
    public bool IsComplete { get; init; }
    public DateTime CompletedDate { get; init; }
    public bool IsAbandoned { get; init; }
    public DateTime AbandonedDate { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormSubmissionUpdateCommand, SurveyFormSubmission>();
    }
}

public record SurveyFormSubmissionUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormSubmissionUpdateCommand, SurveyFormSubmission>
{
    public async Task<SurveyFormSubmission> Handle(SurveyFormSubmissionUpdateCommand command, CancellationToken cancellationToken)
    {
        SurveyFormSubmission entity;
        if (command.Id == null)
        {
            entity = await Context.SurveyFormSubmissions.SingleOrDefaultAsync(e => e.SubmissionUuid == command.SubmissionUuid, cancellationToken);
        }
        else
        {
            entity = await Context.SurveyFormSubmissions.SingleOrDefaultAsync(e => e.SurveyFormSubmissionId == command.Id, cancellationToken);
        }

        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateSurveyFormSubmissionValidator : AbstractValidator<SurveyFormSubmissionUpdateCommand>
{
    public UpdateSurveyFormSubmissionValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.EmployeeId);
        RuleFor(e => e.UserId);
        RuleFor(e => e.SurveyFormId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.StoreId);
    }
}