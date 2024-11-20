namespace Engage.Application.Services.SurveyFormOptions.Queries;

public class SurveyFormOptionOptionQuery : IRequest<List<SurveyFormOptionOption>>
{ 

}

public record SurveyFormOptionOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormOptionOptionQuery, List<SurveyFormOptionOption>>
{
    public async Task<List<SurveyFormOptionOption>> Handle(SurveyFormOptionOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.SurveyFormOptions.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.SurveyFormOptionId)
                              .ProjectTo<SurveyFormOptionOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}