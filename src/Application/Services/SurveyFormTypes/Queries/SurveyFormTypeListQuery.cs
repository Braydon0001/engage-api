namespace Engage.Application.Services.SurveyFormTypes.Queries;

public class SurveyFormTypeListQuery : IRequest<List<SurveyFormTypeDto>>
{

}

public record SurveyFormTypeListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormTypeListQuery, List<SurveyFormTypeDto>>
{
    public async Task<List<SurveyFormTypeDto>> Handle(SurveyFormTypeListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.SurveyFormTypes.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.SurveyFormTypeId)
                              .ProjectTo<SurveyFormTypeDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}