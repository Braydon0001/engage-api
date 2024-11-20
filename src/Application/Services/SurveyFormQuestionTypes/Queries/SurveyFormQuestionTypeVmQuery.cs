namespace Engage.Application.Services.SurveyFormQuestionTypes.Queries;

public record SurveyFormQuestionTypeVmQuery(int Id) : IRequest<SurveyFormQuestionTypeVm>;

public record SurveyFormQuestionTypeVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SurveyFormQuestionTypeVmQuery, SurveyFormQuestionTypeVm>
{
    public async Task<SurveyFormQuestionTypeVm> Handle(SurveyFormQuestionTypeVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.SurveyFormQuestionTypes.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.SurveyFormQuestionTypeId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<SurveyFormQuestionTypeVm>(entity);
    }
}