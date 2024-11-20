
namespace Engage.Application.Services.StoreConcepts.Commands;

public class SurveyFormInsertSubmissionCommand : IRequest<SurveyFormSubmission>, IMapTo<SurveyFormSubmission>
{
    public int StoreId { get; set; }
    public int SurveyFormId { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormInsertSubmissionCommand, SurveyFormSubmission>()
               .ForMember(d => d.StartedDate, opt => opt.MapFrom(s => DateTime.Now))
               .ForMember(d => d.SubmissionUuid, opt => opt.MapFrom(s => ""));
    }
}
public record SurveyFormInsertSubmissionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormInsertSubmissionCommand, SurveyFormSubmission>
{
    public async Task<SurveyFormSubmission> Handle(SurveyFormInsertSubmissionCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<SurveyFormSubmission>(command);

        Context.SurveyFormSubmissions.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class SurveyFormInsertSubmissionValidator : AbstractValidator<SurveyFormInsertSubmissionCommand>
{
    public SurveyFormInsertSubmissionValidator()
    {
        RuleFor(e => e.StoreId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.SurveyFormId).NotEmpty().GreaterThan(0);
    }
}