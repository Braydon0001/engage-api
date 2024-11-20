namespace Engage.Application.Services.SurveyFormOptions.Queries;

public class SurveyFormOptionListQuery : IRequest<List<SurveyFormOptionDto>>
{

}

public record SurveyFormOptionListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormOptionListQuery, List<SurveyFormOptionDto>>
{
    public async Task<List<SurveyFormOptionDto>> Handle(SurveyFormOptionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.SurveyFormOptions.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.SurveyFormOptionId)
                              .ProjectTo<SurveyFormOptionDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}