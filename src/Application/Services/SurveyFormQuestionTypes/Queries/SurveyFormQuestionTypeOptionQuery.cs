namespace Engage.Application.Services.SurveyFormQuestionTypes.Queries;

public class SurveyFormQuestionTypeOptionQuery : IRequest<List<SurveyFormQuestionTypeOption>>
{ 

}

public record SurveyFormQuestionTypeOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormQuestionTypeOptionQuery, List<SurveyFormQuestionTypeOption>>
{
    public async Task<List<SurveyFormQuestionTypeOption>> Handle(SurveyFormQuestionTypeOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.SurveyFormQuestionTypes.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.SurveyFormQuestionTypeId)
                              .ProjectTo<SurveyFormQuestionTypeOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}