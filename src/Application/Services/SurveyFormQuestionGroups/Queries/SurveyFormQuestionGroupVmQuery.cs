namespace Engage.Application.Services.SurveyFormQuestionGroups.Queries;

public record SurveyFormQuestionGroupVmQuery(int GroupId) : IRequest<SurveyFormQuestionGroupVm>;

public record SurveyFormQuestionGroupVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormQuestionGroupVmQuery, SurveyFormQuestionGroupVm>
{
    public async Task<SurveyFormQuestionGroupVm> Handle(SurveyFormQuestionGroupVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.SurveyFormQuestionGroups.Where(e => e.SurveyFormQuestionGroupId == query.GroupId).AsQueryable().AsNoTracking();

        var entity = await queryable.Include(e => e.SurveyForm)
                                    .Include(e => e.SurveyFormQuestionGroupProducts)
                                        .ThenInclude(p => p.EngageVariantProduct)
                                    .Include(e => e.SurveyFormQuestions)
                                    .FirstOrDefaultAsync(cancellationToken);

        return entity == null ? null : Mapper.Map<SurveyFormQuestionGroupVm>(entity);
    }
}