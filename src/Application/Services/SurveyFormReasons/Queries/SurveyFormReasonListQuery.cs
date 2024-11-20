namespace Engage.Application.Services.SurveyFormReasons.Queries;

public class SurveyFormReasonListQuery : IRequest<List<SurveyFormReasonDto>>
{

}

public record SurveyFormReasonListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormReasonListQuery, List<SurveyFormReasonDto>>
{
    public async Task<List<SurveyFormReasonDto>> Handle(SurveyFormReasonListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.SurveyFormReasons.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.SurveyFormReasonId)
                              .ProjectTo<SurveyFormReasonDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}