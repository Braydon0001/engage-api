namespace Engage.Application.Services.SurveyFormQuestions.Queries;

public record SurveyFormQuestionVmQuery(int Id) : IRequest<SurveyFormQuestionVm>;

public record SurveyFormQuestionVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormQuestionVmQuery, SurveyFormQuestionVm>
{
    public async Task<SurveyFormQuestionVm> Handle(SurveyFormQuestionVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.SurveyFormQuestions.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.SurveyFormQuestionGroup)
                             .Include(e => e.SurveyFormQuestionType)
                             .Include(e => e.SurveyFormQuestionProducts)
                                .ThenInclude(e => e.EngageVariantProduct)
                             .Include(e => e.SurveyFormQuestionReasons)
                                .ThenInclude(e => e.SurveyFormReason)
                             .Include(e => e.SurveyFormQuestionOptions)
                                .ThenInclude(e => e.SurveyFormOption);

        var entity = await queryable.SingleOrDefaultAsync(e => e.SurveyFormQuestionId == query.Id, cancellationToken);

        if (entity == null)
        {
            return null;
        }

        var vm = Mapper.Map<SurveyFormQuestionVm>(entity);

        return vm;
    }
}