namespace Engage.Application.Services.SurveyForms.Queries;

public class SurveyFormOptionQuery : IRequest<List<SurveyFormOption>>
{ 

}

public record SurveyFormOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormOptionQuery, List<SurveyFormOption>>
{
    public async Task<List<SurveyFormOption>> Handle(SurveyFormOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.SurveyForms.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.SurveyFormId)
                              .ProjectTo<SurveyFormOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}