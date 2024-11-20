namespace Engage.Application.Services.SurveyFormQuestions.Queries;

public record SurveyFormQuestionsByTypeQuery(int Id, int SurveyFormQuestionTypeId) : IRequest<ListResult<SurveyFormQuestionDto>>;
public record SurveyFormQuestionsByTypeQueryHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormQuestionsByTypeQuery, ListResult<SurveyFormQuestionDto>>
{
    public async Task<ListResult<SurveyFormQuestionDto>> Handle(SurveyFormQuestionsByTypeQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.SurveyFormQuestions.Include(e => e.SurveyFormQuestionGroup).Where(e => e.SurveyFormQuestionGroup.SurveyFormId == query.Id 
                                                                                                    && e.SurveyFormQuestionTypeId == query.SurveyFormQuestionTypeId 
                                                                                                    && !e.Deleted).AsQueryable().AsNoTracking();

        var entities = await queryable.OrderBy(e => e.SurveyFormQuestionGroup.DisplayOrder).ThenBy(e => e.DisplayOrder)
                              .ProjectTo<SurveyFormQuestionDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);

        return new(entities);
    }
}