namespace Engage.Application.Services.SurveyFormQuestionTypes.Queries;

public class SurveyFormQuestionTypeListQuery : IRequest<List<SurveyFormQuestionTypeDto>>
{

}

public record SurveyFormQuestionTypeListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormQuestionTypeListQuery, List<SurveyFormQuestionTypeDto>>
{
    public async Task<List<SurveyFormQuestionTypeDto>> Handle(SurveyFormQuestionTypeListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.SurveyFormQuestionTypes.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.SurveyFormQuestionTypeId)
                              .ProjectTo<SurveyFormQuestionTypeDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}