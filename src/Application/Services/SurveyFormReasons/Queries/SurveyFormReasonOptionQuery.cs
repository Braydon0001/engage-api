namespace Engage.Application.Services.SurveyFormReasons.Queries;

public class SurveyFormReasonOptionQuery : IRequest<List<SurveyFormReasonOption>>
{ 

}

public record SurveyFormReasonOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormReasonOptionQuery, List<SurveyFormReasonOption>>
{
    public async Task<List<SurveyFormReasonOption>> Handle(SurveyFormReasonOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.SurveyFormReasons.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.SurveyFormReasonId)
                              .ProjectTo<SurveyFormReasonOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}