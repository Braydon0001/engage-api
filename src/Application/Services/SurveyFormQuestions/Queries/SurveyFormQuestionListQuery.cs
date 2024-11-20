namespace Engage.Application.Services.SurveyFormQuestions.Queries;

public class SurveyFormQuestionListQuery : IRequest<List<SurveyFormQuestionDto>>
{

}

public record SurveyFormQuestionListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormQuestionListQuery, List<SurveyFormQuestionDto>>
{
    public async Task<List<SurveyFormQuestionDto>> Handle(SurveyFormQuestionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.SurveyFormQuestions.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.SurveyFormQuestionId)
                              .ProjectTo<SurveyFormQuestionDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}