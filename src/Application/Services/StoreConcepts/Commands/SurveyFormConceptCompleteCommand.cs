namespace Engage.Application.Services.StoreConcepts.Commands;

public class SurveyFormConceptCompleteCommand : IRequest<SurveyFormSubmission>
{
    public int Id { get; set; }
}
public record SurveyFormConceptCompleteValidator(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormConceptCompleteCommand, SurveyFormSubmission>
{
    public async Task<SurveyFormSubmission> Handle(SurveyFormConceptCompleteCommand command, CancellationToken cancellationToken)
    {
        var submision = await Context.SurveyFormSubmissions.FirstOrDefaultAsync(e => e.SurveyFormSubmissionId == command.Id, cancellationToken);
        if (submision == null)
        {
            return null;
        }
        submision.IsComplete = true;
        submision.CompletedDate = DateTime.Now;

        await Context.SaveChangesAsync(cancellationToken);

        return submision;
    }
}
