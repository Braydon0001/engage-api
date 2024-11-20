namespace Engage.Application.Services.SurveyFormSubmissions.Queries;

public class SurveyFormSubmissionListQuery : IRequest<List<SurveyFormSubmissionDto>>
{

}

public record SurveyFormSubmissionListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormSubmissionListQuery, List<SurveyFormSubmissionDto>>
{
    public async Task<List<SurveyFormSubmissionDto>> Handle(SurveyFormSubmissionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.SurveyFormSubmissions.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.SurveyFormSubmissionId)
                              .ProjectTo<SurveyFormSubmissionDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}