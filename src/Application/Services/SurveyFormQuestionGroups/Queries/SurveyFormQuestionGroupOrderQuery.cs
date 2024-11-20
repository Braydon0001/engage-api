namespace Engage.Application.Services.SurveyFormQuestionGroups.Queries;

public record SurveyQuestionGroupOrderQuery(int SurveyId) : IRequest<List<SurveyFormQuestionGroupOrderDto>>;

public record SurveyQuestionGroupOrderHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyQuestionGroupOrderQuery, List<SurveyFormQuestionGroupOrderDto>>
{
    public async Task<List<SurveyFormQuestionGroupOrderDto>> Handle(SurveyQuestionGroupOrderQuery query, CancellationToken cancellationToken)
    {
        var entities = await Context.SurveyFormQuestionGroups
                                        .Where(e => e.SurveyFormId == query.SurveyId)
                                        .OrderBy(e => e.DisplayOrder)
                                        .ProjectTo<SurveyFormQuestionGroupOrderDto>(Mapper.ConfigurationProvider)
                                        .ToListAsync(cancellationToken);

        return entities == null ? null : entities;
    }
}