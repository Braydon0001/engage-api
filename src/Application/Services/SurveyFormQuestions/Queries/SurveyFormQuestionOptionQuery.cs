namespace Engage.Application.Services.SurveyFormQuestions.Queries;

public class SurveyFormQuestionOptionQuery : IRequest<List<SurveyFormQuestionOption>>
{ 

}

public record SurveyFormQuestionOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormQuestionOptionQuery, List<SurveyFormQuestionOption>>
{
    public async Task<List<SurveyFormQuestionOption>> Handle(SurveyFormQuestionOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.SurveyFormQuestions.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.SurveyFormQuestionId)
                              .ProjectTo<SurveyFormQuestionOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}