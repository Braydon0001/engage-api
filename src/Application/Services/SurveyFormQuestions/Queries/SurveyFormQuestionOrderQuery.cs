namespace Engage.Application.Services.SurveyFormQuestions.Queries;

public record SurveyFormQuestionOrderQuery(int GroupId) : IRequest<List<SurveyFormQuestionOrderDto>>;

public record SurveyFormQuestionOrderHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormQuestionOrderQuery, List<SurveyFormQuestionOrderDto>>
{
    public async Task<List<SurveyFormQuestionOrderDto>> Handle(SurveyFormQuestionOrderQuery query, CancellationToken cancellationToken)
    {
        var entities = await Context.SurveyFormQuestions
                                        .Where(e => e.SurveyFormQuestionGroupId == query.GroupId)
                                        .OrderBy(e => e.DisplayOrder)
                                        .ProjectTo<SurveyFormQuestionOrderDto>(Mapper.ConfigurationProvider)
                                        .ToListAsync(cancellationToken);

        return entities == null ? null : entities;
    }
}