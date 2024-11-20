namespace Engage.Application.Services.SurveyFormSubmissions.Queries;

public class SurveyFormSubmissionOptionQuery : IRequest<List<SurveyFormSubmissionOption>>
{ 

}

public record SurveyFormSubmissionOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormSubmissionOptionQuery, List<SurveyFormSubmissionOption>>
{
    public async Task<List<SurveyFormSubmissionOption>> Handle(SurveyFormSubmissionOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.SurveyFormSubmissions.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.SurveyFormSubmissionId)
                              .ProjectTo<SurveyFormSubmissionOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}