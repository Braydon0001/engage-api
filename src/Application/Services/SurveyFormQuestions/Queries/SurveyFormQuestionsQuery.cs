namespace Engage.Application.Services.SurveyFormQuestions.Queries;

public record SurveyFormQuestionsQuery(int Id) : IRequest<ListResult<SurveyFormQuestionDto>>;
public record SurveyFormQuestionsQueryHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormQuestionsQuery, ListResult<SurveyFormQuestionDto>>
{
    public async Task<ListResult<SurveyFormQuestionDto>> Handle(SurveyFormQuestionsQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.SurveyFormQuestions.Include(e => e.SurveyFormQuestionGroup).Where(e => e.SurveyFormQuestionGroup.SurveyFormId == query.Id && !e.Deleted).AsQueryable().AsNoTracking();

        var entities = await queryable.OrderBy(e => e.SurveyFormQuestionGroup.DisplayOrder).ThenBy(e => e.DisplayOrder)
                              .ProjectTo<SurveyFormQuestionDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);

        //var emptyGroups = await Context.SurveyFormQuestionGroups.Include(e => e.SurveyFormQuestions).Where(e => e.SurveyFormId == query.Id && !e.SurveyFormQuestions.Any()).ToListAsync(cancellationToken);

        ////create an empty entry for each group
        //foreach (var group in emptyGroups)
        //{
        //    var emptyEntry = new SurveyFormQuestionDto()
        //    {
        //        SurveyFormQuestionGroupId = group.SurveyFormQuestionGroupId,
        //        SurveyFormQuestionGroupName = group.Name,
        //        Metadata = new List<JsonSetting> { new JsonSetting() { Name = "EmptyGroup", Value = "true" } }
        //    };
        //    entities.Add(emptyEntry);
        //}

        return new(entities);
    }
}