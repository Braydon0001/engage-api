namespace Engage.Application.Services.SurveyFormQuestionGroups.Queries;

public class SurveyFormQuestionGroupListQuery : IRequest<List<SurveyFormQuestionGroupDto>>
{

}

public record SurveyFormQuestionGroupListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormQuestionGroupListQuery, List<SurveyFormQuestionGroupDto>>
{
    public async Task<List<SurveyFormQuestionGroupDto>> Handle(SurveyFormQuestionGroupListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.SurveyFormQuestionGroups.AsQueryable().AsNoTracking();

        return await queryable.OrderBy(e => e.SurveyFormQuestionGroupId)
                              .ProjectTo<SurveyFormQuestionGroupDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}