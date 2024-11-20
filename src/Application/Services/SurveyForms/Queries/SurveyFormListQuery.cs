namespace Engage.Application.Services.SurveyForms.Queries;

public class SurveyFormListQuery : IRequest<List<SurveyFormDto>>
{

}

public record SurveyFormListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormListQuery, List<SurveyFormDto>>
{
    public async Task<List<SurveyFormDto>> Handle(SurveyFormListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.SurveyForms.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.SurveyFormId)
                              .ProjectTo<SurveyFormDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}