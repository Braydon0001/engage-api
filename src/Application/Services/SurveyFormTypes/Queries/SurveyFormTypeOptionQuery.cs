namespace Engage.Application.Services.SurveyFormTypes.Queries;

public class SurveyFormTypeOptionQuery : IRequest<List<SurveyFormTypeOption>>
{ 

}

public record SurveyFormTypeOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormTypeOptionQuery, List<SurveyFormTypeOption>>
{
    public async Task<List<SurveyFormTypeOption>> Handle(SurveyFormTypeOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.SurveyFormTypes.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.SurveyFormTypeId)
                              .ProjectTo<SurveyFormTypeOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}