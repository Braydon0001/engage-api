namespace Engage.Application.Services.SurveyFormAnswers.Queries;

public class SurveyFormAnswerListQuery : IRequest<List<SurveyFormAnswerDto>>
{

}

public record SurveyFormAnswerListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormAnswerListQuery, List<SurveyFormAnswerDto>>
{
    public async Task<List<SurveyFormAnswerDto>> Handle(SurveyFormAnswerListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.SurveyFormAnswers.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.SurveyFormAnswerId)
                              .ProjectTo<SurveyFormAnswerDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}