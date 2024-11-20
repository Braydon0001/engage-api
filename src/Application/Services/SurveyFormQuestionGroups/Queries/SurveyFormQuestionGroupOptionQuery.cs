namespace Engage.Application.Services.SurveyFormQuestionGroups.Queries;

public record SurveyFormQuestionGroupOptionQuery(int Id) : IRequest<List<SurveyFormQuestionGroupOption>>;

public record SurveyFormQuestionGroupOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormQuestionGroupOptionQuery, List<SurveyFormQuestionGroupOption>>
{
    public async Task<List<SurveyFormQuestionGroupOption>> Handle(SurveyFormQuestionGroupOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.SurveyFormQuestionGroups.Where(e => e.SurveyFormId == query.Id).AsQueryable().AsNoTracking();

        return await queryable.OrderBy(e => e.SurveyFormQuestionGroupId)
                              .ProjectTo<SurveyFormQuestionGroupOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}