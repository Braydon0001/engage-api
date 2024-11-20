namespace Engage.Application.Services.SurveyFormQuestionGroups.Queries;

public record SurveyQuestionGroupListQuery(int SurveyId) : IRequest<List<SurveyFormQuestionGroupDto>>;

public record SurveyQuestionGroupListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyQuestionGroupListQuery, List<SurveyFormQuestionGroupDto>>
{
    public async Task<List<SurveyFormQuestionGroupDto>> Handle(SurveyQuestionGroupListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Context.SurveyFormQuestionGroups
                                        .Include(e => e.SurveyForm)
                                        .Include(e => e.SurveyFormQuestions)
                                        .Where(e => e.SurveyFormId == query.SurveyId)
                                        .OrderBy(e => e.DisplayOrder)
                                        .ProjectTo<SurveyFormQuestionGroupDto>(Mapper.ConfigurationProvider)
                                        .ToListAsync(cancellationToken);

        return entities == null ? null : entities;
    }
}